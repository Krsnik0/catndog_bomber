using UnityEngine;
using System.Collections;

public class DragInputEvent : InputEvent {

	private Vector3 _touchPos;
	private Vector3 _dragDir;

	public DragInputEvent( AbstractGameObject target_, Vector3 touchPos_, Vector3 dragDir_ ) : base( target_ )
	{
		this._touchPos = touchPos_;
		this._dragDir = dragDir_;
	}

	public override InputType inputType {
		get {
			return InputType.DRAG;
		}
	}

	public override Vector3 touchPosition {
		get {
			return _touchPos;
		}
	}

	public Vector3 dragDirection {
		get {
			return _dragDir;
		}
	}
}
