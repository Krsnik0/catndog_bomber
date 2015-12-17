using UnityEngine;
using System.Collections;

public class BombExplodeEvent : AbstractEvent {
	public const string BOMB_EXPLODE_EVENT_KEY = "bombExplode";
	
	public BombExplodeEvent( AbstractBomb target_ ) : base( target_ )
	{
	}
	
	public AbstractBomb targetGameObject
	{
		get
		{
			if( target != null )
			{
				return target as AbstractBomb;
			}
			else
			{
				return null;
			}
		}
	}
	
	public override string eventKey {
		get {
			return BOMB_EXPLODE_EVENT_KEY;
		}
	}
}
