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
		switch (GameManager.getInstance ().currentState) {
		case GameManager.GameState.PLAYING:
			if (event_.target is GameMap) {
				setPath (PathFinder.findPath( GameMap.getInstance(), positionIndexPair, PositionCalcUtil.vector3ToMapIndex (Camera.main.ScreenToWorldPoint( Input.mousePosition ) ) ) );
			}
			break;
		}
	}

	/*public override bool triggerInput (InputEvent input_)
	{
		switch (GameManager.getInstance ().currentState) {
		case GameManager.GameState.PLAYING:
			setPath (PathFinder.findPath( GameMap.getInstance(), positionIndexPair, PositionCalcUtil.vector3ToMapIndex (input_.touchPosition) ) );
			break;
		}
		return false;
	}*/

	public override void destroyObject ()
	{
		GameMap.getInstance ().removeObject (this);
		Destroy (gameObject);
	}

	public override float speed {
		get {
			return 3;
		}
	}

	public override void onExplosion (BombValueObject bombData_)
	{
		destroyObject ();
	}
}
