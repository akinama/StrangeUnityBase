using System;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace vt.sampleproject
{
	public interface ISampleService
	{
		void SavePlayerPos(Vector2 pos);
		IEventDispatcher dispatcher{get;set;}
	}
}

