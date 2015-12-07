using UnityEngine;
using System.Collections;

public class MarkerBlock : AbstractBlock {

	public bool isTouchable { get; set; }
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
			return GameMap.GameMapLayer.MARKER;
		}
	}

	protected override void initObject ()
	{
	}

	protected override void updateObject ()
	{
	}

	public override void onStateEnd (GameManager.GameState gameState_)
	{
		switch (gameState_) {
		case GameManager.GameState.THROWING:
			destroyObject();
			break;
		}
	}

	public override void onStateStart (GameManager.GameState gameState_)
	{
	}

	/*public override bool triggerInput (InputEvent input_)
	{
		if (isTouchable) {
			switch (input_.inputType) {
			case InputEvent.InputType.TOUCH:
				IntegerPair clickIdx = PositionCalcUtil.vector3ToMapIndex (input_.touchPosition);
				
				if( clickIdx.ToString () == positionIndexPair.ToString () )
				{
					((TouchInputEvent)input_).selectedObject = this;
					return true;
				}
				else
				{
					return false;
				}
			case InputEvent.InputType.DRAG:
				return false;
			}
		}

		return false;
	}*/

	public override void destroyObject ()
	{
		GameMap.getInstance ().removeObject (this);
		GameObject.Destroy (gameObject);
	}
}

