using UnityEngine;
using System.Collections;

public class ObjectRemovedEvent : AbstractEvent {

	public const string OBJECT_REMOVED_EVENT_KEY = "objectRemoved";

	public ObjectRemovedEvent( AbstractGameObject target_ ) : base( target_ )
	{
	}

	public AbstractGameObject targetGameObject
	{
		get
		{
			if( target != null )
			{
				return target as AbstractGameObject;
			}
			else
			{
				return null;
			}
		}
	}

	public override string eventKey {
		get {
			return OBJECT_REMOVED_EVENT_KEY;
		}
	}
}
