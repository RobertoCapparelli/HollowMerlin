﻿using UnityEngine;
using System.Collections;

namespace ICode.Actions.Variable{
	[Category(Category.Variable)]
	[Tooltip("Sets the GameObject value of a variable.")]
	[System.Serializable]
	public class SetGameObject : StateAction {
		[Shared]
		[Tooltip("The variable to use.")]
		public FsmGameObject variable;
		[Tooltip("The value to set.")]
		public FsmGameObject value;
		[Tooltip("Execute the action every frame.")]
		public bool everyFrame;
		
		public override void OnEnter ()
		{
			variable.Value = value.Value;
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			variable.Value = value.Value;
		}
	}
}