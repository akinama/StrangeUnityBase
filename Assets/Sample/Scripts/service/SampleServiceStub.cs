using System;
using System.Collections;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace vt.sampleproject
{
	public class SampleServiceStub : ISampleService
	{
		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{get;set;}
		
		[Inject]
		public IEventDispatcher dispatcher{get;set;}
		
		// Save the player's position.
		public void SavePlayerPos(Vector2 pos)
		{
			// Fake a web call.
			MonoBehaviour root = contextView.GetComponent<SampleRoot>();
			root.StartCoroutine(waitASecond(pos));
		}
		
		// Fake a web call by waiting for one second.
		private IEnumerator waitASecond(Vector2 pos)
		{
			yield return new WaitForSeconds(1f);

			// Stub save.
			PlayerPrefs.SetFloat("GridPosX", pos.x);
			PlayerPrefs.SetFloat("GridPosY", pos.y);
			PlayerPrefs.Save();

			dispatcher.Dispatch(SampleEvent.PLAYER_POSITION_SAVED, pos);
		}
	}
}

