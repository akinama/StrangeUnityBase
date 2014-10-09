using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

namespace vt.sampleproject
{
	public class StartCommand : EventCommand
	{
		
		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{get;set;}

		[Inject]
		public ISampleModel model{get;set;}
		
		public override void Execute()
		{
			PopulateModels();
		}

		// Read persisted data, update model. This should live in a util class.
		private void PopulateModels()
		{
			model.gridPos = new Vector2(
										PlayerPrefs.GetFloat("GridPosX", 0.0f),
										PlayerPrefs.GetFloat("GridPosY", 0.0f)
										);

			model.position = model.gridPos * 5.0f + new Vector2(2.5f, 2.5f);

			dispatcher.Dispatch(SampleEvent.PLAYER_MOVED, model.position);
			dispatcher.Dispatch(SampleEvent.PLAYER_POSITION_SAVED, model.gridPos);
		}
	}
}

