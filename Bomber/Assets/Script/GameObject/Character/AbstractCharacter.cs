using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractCharacter : AbstractGameObject {

	private List<AStarPath> _path;

	public override bool isObstacle {
		get {
			return false;
		}
	}

	public override GameMap.GameMapLayer layer {
		get {
			return GameMap.GameMapLayer.OBJECT;
		}
	}

	public abstract float speed{ get; }

	public void setPath( List<AStarPath> path )
	{
		this._path = path;
	}

	protected override void updateObject ()
	{
		switch (GameManager.getInstance ().currentState) {
		case GameManager.GameState.PLAYING:
			if (_path != null) {
				Vector3 dst = PositionCalcUtil.mapIndexToVector3 (_path [0]);
				Vector3 delta = (dst - transform.position).normalized * speed * Time.deltaTime;
				transform.position += delta;
				
				if( Vector3.Distance( dst, transform.position ) < 0.03f )
				{
					//Debug.Log( "next destination = " + dst.ToString() );
					transform.position = dst;
					_path.RemoveAt( 0 );
					
					if( _path.Count == 0 )
					{
						Debug.Log( "arrived" );
						_path = null;
					}
				}
			}
			break;
		}
	}

	public override void onStateEnd (GameManager.GameState gameState_)
	{
	}

	public override void onStateStart (GameManager.GameState gameState_)
	{
	}

	public override void destroyObject ()
	{
		GameMap.getInstance ().removeObject (this);
		Destroy (this);
	}
}
