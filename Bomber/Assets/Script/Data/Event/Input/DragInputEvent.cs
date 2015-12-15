using UnityEngine;
using System.Collections;

public class DragInputEvent : InputEvent {
	
	private Vector3 _dragDir;

	public DragInputEvent( AbstractBoomscapeObject target_, Vector3 dragDir_ ) : base( target_ )
	{
		this._dragDir = dragDir_;
	}

	public override InputType inputType {
		get {
			return InputType.DRAG;
		}
	}

	public Vector3 dragDirection {
		get {
			return _dragDir;
		}
	}
}
