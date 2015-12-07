using UnityEngine;
using System.Collections;

public abstract class InputEvent : AbstractEvent {

	public enum InputType
	{
		TOUCH,
		DRAG
	};

	public InputEvent( AbstractGameObject target_ ) : base( target_ )
	{
	}

	public abstract InputType inputType { get; }
	public abstract Vector3 touchPosition { get; }
}
