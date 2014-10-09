using System;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace vt.sampleproject
{
	public class InputDetector : EventView
	{
		public const string CLICK = "CLICK";
		public const string MOVE = "MOVE";
		
		void OnMouseDown()
		{
			dispatcher.Dispatch(CLICK);
		}

		void Update()
		{
			float xSpeed = 0.2f;
			float zSpeed = 0.2f;
			float tiny = 0.001f;
			float xMove = Input.GetAxis("Horizontal") * xSpeed;
			float zMove = Input.GetAxis("Vertical") * zSpeed;
			
			if (Mathf.Abs(xMove) > tiny || Mathf.Abs(zMove) > tiny)
			{
				dispatcher.Dispatch(MOVE, new Vector2(xMove, zMove));
			}
		}
	}
}

