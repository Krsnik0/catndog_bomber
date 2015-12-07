using UnityEngine;
using System.Collections;

public class NormalBomb : AbstractBomb {

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
		updateObject ();
	}

	protected override void initObject ()
	{
		base.initObject ();
	}

	public override void onExplosion (BombValueObject bombData_)
	{
		explode ();
	}

	public override void onStateEnd (GameManager.GameState gameState_)
	{
	}

	public override void onStateStart (GameManager.GameState gameState_)
	{
	}

	public override bool triggerInput (InputEvent input_)
	{
		return false;
	}

	public override void destroyObject ()
	{
		GameMap.getInstance ().removeObject (this);
		Destroy (gameObject);
	}
}
