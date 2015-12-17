using UnityEngine;
using System.Collections;

public class PlayerCharacter : AbstractCharacter {

	private bool _playerInitFlag = false;

	private void Start()
	{
		initObject ();
	}

	private void Update()
	{
		updateObject ();
	}

	protected override void initObject ()
	{
		if (!_playerInitFlag) {
			_playerInitFlag = true;

			EventManager.getInstance().addEventListener( InputEvent.INPUT_EVENT_KEY, onInputEvent );
		}
	}

	protected override void updateObject ()
	{
		base.updateObject ();
	}

	private void onInputEvent( AbstractEvent event_ )
	{
        InputEvent inputEvent = event_ as InputEvent;

		if ( inputEvent.inputType == InputEvent.InputType.TOUCH && event_.target is GameMap) {
			setPath (PathFinder.findPath( GameMap.getInstance(), positionIndexPair, PositionCalcUtil.vector3ToMapIndex (Camera.main.ScreenToWorldPoint( Input.mousePosition ) ) ) );
		}
	}

	public override float speed {
		get {
			return 3;
		}
	}

	public override void onExplosion (AbstractBombValueObject bombData_)
	{
		destroyObject ();
	}
}
