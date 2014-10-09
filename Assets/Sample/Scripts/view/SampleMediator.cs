/// Example mediator
/// =====================
/// Make your Mediator as thin as possible. Its function is to mediate
/// between view and app. Don't load it up with behavior that belongs in
/// the View (listening to/controlling interface), Commands (business logic),
/// Models (maintaining state) or Services (reaching out for data).

using System;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace vt.sampleproject
{
	public class SampleMediator : EventMediator
	{
		//This is how your Mediator knows about your View.
		[Inject]
		public SampleView view{ get; set;}
		
		public override void OnRegister()
		{
			//Listen to the view for an event
			view.dispatcher.AddListener(SampleView.MOVE_EVENT, onRequestMove);
			
			//Listen to the global event bus for events
			dispatcher.AddListener(SampleEvent.PLAYER_MOVED, onPlayerMoved);
			dispatcher.AddListener(SampleEvent.PLAYER_POSITION_SAVED, onPlayerPositionSaved);
			
			view.init ();
		}
		
		public override void OnRemove()
		{
			//Clean up listeners when the view is about to be destroyed
			dispatcher.RemoveListener(SampleEvent.PLAYER_MOVED, onPlayerMoved);
			dispatcher.RemoveListener(SampleEvent.PLAYER_POSITION_SAVED, onPlayerPositionSaved);
		}

		// View -> Mediator
		private void onRequestMove(IEvent evt)
		{
			dispatcher.Dispatch(SampleEvent.REQUEST_PLAYER_MOVE, evt.data);
		}

		// Command -> Mediator
		private void onPlayerMoved(IEvent evt)
		{
			Vector2 newPosition = (Vector2)evt.data;
			view.updatePlayerPosition(newPosition);
		}

		// Command -> Mediator
		private void onPlayerPositionSaved(IEvent evt)
		{
			Vector2 newPosition = (Vector2)evt.data;
			view.updateSavedPlayerPosition(newPosition);
		}
	}
}

