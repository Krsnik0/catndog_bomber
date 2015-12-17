using UnityEngine;
using System.Collections;

public abstract class AbstractEvent {
	private AbstractBoomscapeObject _target;

	public AbstractEvent( AbstractBoomscapeObject target_ )
	{
		_target = target_;
	}

	public AbstractEvent()
	{
		_target = null;
	}

	public AbstractBoomscapeObject target {
		get {
			return _target;
		}
	}
	
	abstract public string eventKey { get; }
}
