using UnityEngine;
using System.Collections;

public class NormalBlock : AbstractBlock {

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
		updateObject ();
	}

	protected override void updateObject ()
	{
		switch (_blockState) {
		case BlockState.DESTROYING:
			Color c = _spriteRenderer.material.color;
			c.a -= 5f * Time.deltaTime;
			_spriteRenderer.material.color = c;
			break;
		}
	}

	public override void onExplosion (AbstractBombValueObject bombData_)
	{
		_blockState = BlockState.DESTROYING;
		Invoke ("destroyObject", 0.2f);
	}

	public override void destroyObject ()
	{
		EventManager.getInstance().dispatchEvent( new ObjectRemovedEvent( this ) );
		Destroy (gameObject);
	}
}
