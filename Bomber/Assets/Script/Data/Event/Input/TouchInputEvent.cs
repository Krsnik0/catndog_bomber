using UnityEngine;
using System.Collections;

public class TouchInputEvent : InputEvent {

	private Vector3 _pos;
	public AbstractGameObject selectedObject { get; set; }

	public TouchInputEvent( AbstractGameObject target_, Vector3 pos_ ) : base( target_ )
	{
		this._pos = pos_;
	}

	public override InputType inputType {
		get {
			return InputType.TOUCH;
		}
	}

	public override Vector3 touchPosition {
		get {
			return _pos;
		}
	}
}
