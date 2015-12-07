using UnityEngine;
using System.Collections;
using System.Xml;

public class GameMap : AbstractContainerObject {

	public enum GameMapLayer { TILE, OBJECT, MARKER };

	static private GameMap _instance;

	public IntegerPair mapSize { get; set; }

	private bool _mapInitFlag = false;

	private MarkerLayer _markerLayer;
	private ObjectLayer _objectLayer;
	private TileLayer _tileLayer;

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static public GameMap getInstance()
	{
		return _instance;
	}

	protected override void initObject ()
	{
		base.initObject ();

		if (!_mapInitFlag) {
			_mapInitFlag = true;

			_instance = this;

			_markerLayer = GetComponentInChildren<MarkerLayer>();
			_objectLayer = GetComponentInChildren<ObjectLayer>();
			_tileLayer = GetComponentInChildren<TileLayer>();

			addObject( _markerLayer );
			addObject( _objectLayer );
			addObject( _tileLayer );
		}
	}

	public void loadStage( StageValueObject mapData_ )
	{
		mapSize = new IntegerPair( mapData_.objLayer.Length, mapData_.objLayer[0].Length );
		_objectLayer.loadLayer (mapData_.objLayer);
		_tileLayer.loadEmptyLayer (mapSize);
		_markerLayer.loadEmptyLayer (mapSize);
	}

	public bool isMovable( int x_, int y_ )
	{
		if (0 <= x_ && x_ < mapSize.x &&
			0 <= y_ && y_ < mapSize.y) {
			return !_objectLayer.isObjectExistAt (x_, y_);
		} else {
			return false;
		}
	}

	public bool isThrowable( int x_, int y_ )
	{
		return isMovable (x_, y_);
	}

	public override AbstractGameObject addObject (AbstractGameObject obj_)
	{
		switch (obj_.layer) {
		case GameMapLayer.MARKER:
			return _markerLayer.addObject( obj_ );
		case GameMapLayer.OBJECT:
			return _objectLayer.addObject( obj_ );
		case GameMapLayer.TILE:
			return _tileLayer.addObject( obj_ );
		}

		return null;
	}

	public override AbstractGameObject removeObject (AbstractGameObject obj_)
	{
		switch (obj_.layer) {
		case GameMapLayer.MARKER:
			return _markerLayer.removeObject( obj_ );
		case GameMapLayer.OBJECT:
			return _objectLayer.removeObject( obj_ );
		case GameMapLayer.TILE:
			return _tileLayer.removeObject( obj_ );
		}
		
		return null;
	}

	public override bool contains (AbstractGameObject obj_)
	{
		switch (obj_.layer) {
		case GameMapLayer.MARKER:
			return _markerLayer.contains( obj_ );
		case GameMapLayer.OBJECT:
			return _objectLayer.contains( obj_ );
		case GameMapLayer.TILE:
			return _tileLayer.contains( obj_ );
		}
		
		return false;
	}

	public override void moveObject (AbstractGameObject obj_, IntegerPair dst_ )
	{
		switch (obj_.layer) {
		case GameMapLayer.MARKER:
			_markerLayer.moveObject( obj_, dst_ );
			break;
		case GameMapLayer.OBJECT:
			_objectLayer.moveObject( obj_, dst_ );
			break;
		case GameMapLayer.TILE:
			_tileLayer.moveObject( obj_, dst_ );
			break;
		}
	}

	public void explode( AbstractBomb bomb_ )
	{
		_objectLayer.explode (bomb_);
	}

	public override bool triggerInput (InputEvent input_)
	{
		switch (input_.inputType) {
		case InputEvent.InputType.DRAG:
			return base.triggerInput( input_ );
		case InputEvent.InputType.TOUCH:
			bool retValue = base.triggerInput (input_);
			TouchInputEvent touchInput = ((TouchInputEvent)input_);

			if( touchInput.selectedObject is MarkerBlock )
			{
				IntegerPair posIdxPair = touchInput.selectedObject.positionIndexPair;
				BombValueObject bombData = RootUI.getInstance().uiData.selectedBomb;

				if( _markerLayer.isSelectedBlock( posIdxPair ) )
				{
					AbstractBomb bomb = GameObjectFactory.getInstance().generateObject( bombData.prefabData.Value ).GetComponent<AbstractBomb>();
					bomb.positionIndexPair = posIdxPair.clone();
					bomb.bombData = bombData;
					bomb.startCountdown();
					addObject( bomb );

					GameManager.getInstance().changeState( GameManager.GameState.PLAYING );
				}
				else
				{
					_markerLayer.removeAll ( true );
					_markerLayer.markThrowableArea (_objectLayer.playerCharacter.positionIndexPair, RootUI.getInstance().uiData.selectedBomb.throwRange, new Color( 0f, 1f, 0f, 0.3f ) );
					_markerLayer.markExplosionArea ( posIdxPair,
					                                bombData.explosionShape,
					                                bombData.bombPosition,
					                                new Color( 1f, 0f, 0f, 0.3f ) );
				}

			}
			return retValue;
		}
		return false;
	}

	public void updateMap()
	{
		switch (GameManager.getInstance ().currentState) {
		case GameManager.GameState.THROWING:
			_markerLayer.removeAll ( true );
			_markerLayer.markThrowableArea (_objectLayer.playerCharacter.positionIndexPair, RootUI.getInstance().uiData.selectedBomb.throwRange, new Color( 0f, 1f, 0f, 0.3f ) );
			break;
		}
	}
}
