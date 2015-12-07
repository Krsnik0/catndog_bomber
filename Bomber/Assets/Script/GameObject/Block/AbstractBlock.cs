using UnityEngine;
using System.Collections;

public abstract class AbstractBlock : AbstractGameObject {

	protected enum BlockState { NORMAL, DESTROYING, MOVING };
	protected BlockState _blockState { get; set; }

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
		updateObject ();
	
	}

	public override GameMap.GameMapLayer layer {
		get {
			return GameMap.GameMapLayer.OBJECT;
		}
	}

	public override bool isObstacle {
		get {
			return true;
		}
	}

  	public override void onExplosion (BombValueObject bombData_)
	{
		destroyObject ();
	}
}
