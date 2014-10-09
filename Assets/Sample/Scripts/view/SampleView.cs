using System;
using System.Collections;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace vt.sampleproject
{
	public class SampleView : View
	{
		internal const string MOVE_EVENT = "MOVE_EVENT";
		
		[Inject]
		public IEventDispatcher dispatcher{get;set;}
		
		internal void init()
		{
			GameObject player = GameObject.FindWithTag("Player");
			
			InputDetector inputs = player.GetComponent<InputDetector>() as InputDetector;
			inputs.dispatcher.AddListener(InputDetector.MOVE, onMoveRequest);
		}

		void Update()
		{
		}

		// Keyboard -> View
		void onMoveRequest(IEvent evt)
		{
			Vector2 direction = (Vector2)evt.data;
			dispatcher.Dispatch(MOVE_EVENT, direction);
		}

		// Controller -> Mediator -> View
		internal void updatePlayerPosition(Vector2 position)
		{
			GameObject go = GameObject.FindWithTag("Player");

			Vector3 newPos = go.transform.localPosition;
			newPos.x = position.x;
			newPos.y = position.y;
			go.transform.localPosition = newPos;
		}

		// Controller -> Mediator -> View
		internal void updateSavedPlayerPosition(Vector2 gridPos)
		{
			Vector2 position = gridPos * 5.0f + new Vector2(2.5f, 2.5f);

			GameObject go = GameObject.FindWithTag("PlayerSaved");

			Vector3 newPos = go.transform.localPosition;
			newPos.x = position.x;
			newPos.y = position.y;
			go.transform.localPosition = newPos;
		}
	}
}

