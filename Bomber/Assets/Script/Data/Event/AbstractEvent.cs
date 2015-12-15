using UnityEngine;
using System.Collections;

public abstract class AbstractEvent {
	private AbstractBoomscapeObject _target;

	public AbstractEvent( AbstractBoomscapeObject target_ )
	{
		_target = target_;
	}

	public AbstractBoomscapeObject target {
		get {
			return _target;
		}
	}

	public bool isBubbling { get; set; }
	abstract public string eventKey { get; }
}
