using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace vt.sampleproject
{
	public class SampleMoveCommand : EventCommand
	{
		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{get;set;}

		[Inject]
		public ISampleModel model{get;set;}
		
		[Inject]
		public ISampleService service{get;set;}
		
		// Are we currently saving data?
		private static bool nowSaving = false;

		// Pending save requests
		private static Queue<IEnumerator> saveQueue = new Queue<IEnumerator>();

		public SampleMoveCommand()
		{
		}
		
		// View -> Mediator -> Command
		public override void Execute()
		{
			Vector2 oldPos = model.position;
			Vector2 pos = oldPos;
			Vector2 direction = (Vector2)evt.data;
			pos.x += direction.x;
			pos.y += direction.y;

			if (!CanMove(oldPos, pos))
			{
				return;
			}

			model.position = pos;

			if (ShouldSave(oldPos, pos))
			{
				model.gridPos = GetGridPos(pos);
				SaveGridPos(model.gridPos);
			}

			dispatcher.Dispatch(SampleEvent.PLAYER_MOVED, model.position);
		}

		// Can we move?
		private bool CanMove(Vector2 oldPos, Vector2 newPos)
		{
			return true;
		}
		
		// Should we persist the position info?
		private bool ShouldSave(Vector2 oldPos, Vector2 newPos)
		{
			return (GetGridPos(oldPos) != GetGridPos(newPos));
		}

		// World coords -> Grid position
		private Vector2 GetGridPos(Vector2 worldPos)
		{
			return new Vector2(Mathf.Floor(worldPos.x / 5), Mathf.Floor(worldPos.y / 5));
		}

		// Persist the grid location
		private void SaveGridPos(Vector2 gridPos)
		{
			// Hold command object in memory to wait for async results.
			Retain();

			IEnumerator saver = WaitToSave(gridPos);
			if (saver.MoveNext())
			{
				// Could not save, enqueue and wait.
				saveQueue.Enqueue(saver);
			}
		}

		// Wait our turn.
		private IEnumerator WaitToSave(Vector2 gridPos)
		{
			if (nowSaving)
			{
				// Already mid-request. Wait.
				yield return true;
			}

			// Clear to save.
			nowSaving = true;

			// Call the service. Listen for a response
			service.dispatcher.AddListener(SampleEvent.PLAYER_POSITION_SAVED, onComplete);
			service.SavePlayerPos(gridPos);
		}

		// Finished saving. Fire off the next request and clean up.
		private void onComplete(IEvent evt)
		{
			service.dispatcher.RemoveListener(SampleEvent.PLAYER_POSITION_SAVED, onComplete);
			
			dispatcher.Dispatch(SampleEvent.PLAYER_POSITION_SAVED, (Vector2)evt.data);

			if (saveQueue.Count > 0)
			{
				saveQueue.Dequeue().MoveNext();
			}
			else
			{
				nowSaving = false;
			}

			// Command object leaks without this call.
			Release();
		}
	}
}

