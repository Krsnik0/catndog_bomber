using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractLayer : AbstractContainerObject {

	private bool _layerInitFlag = false;
	private bool _layerFirstUpdateFlag;

	private AbstractGameObject[][] _obstacleHashmap;
	protected List<AbstractGameObject> _nonObstacleObjects;

	private IntegerPair _size;

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

		if (!_layerInitFlag) {
			_layerFirstUpdateFlag = true;

			_nonObstacleObjects = new List<AbstractGameObject>();
		}
	}

	protected override void updateObject ()
	{
		base.updateObject ();
		
		if (_layerFirstUpdateFlag) {
		}
	}

	public virtual void loadEmptyLayer( IntegerPair size_ )
	{
		_size = size_.clone ();

		_obstacleHashmap = new AbstractGameObject[size_.x][];
	
		int i;
		for( i = 0; i < size_.x; ++ i )
		{
			_obstacleHashmap[i] = new AbstractGameObject[size_.y];
		}
	}

	public virtual void loadLayer( AbstractGameObjectValueObject[][] layerData_ )
	{
		int i,j;

		_obstacleHashmap = new AbstractGameObject[layerData_.Length][];
		
		for( i = 0; i < layerData_.Length; ++ i )
		{
            _obstacleHashmap[i] = new AbstractGameObject[layerData_[i].Length];
		}

		GameObject gameObj;
		for (i = 0; i < layerData_.Length; ++ i) {
			for( j = 0; j < layerData_[i].Length; ++ j )
			{
				if( layerData_[i][j] != null )
				{
					gameObj = GameObjectFactory.getInstance().generateObject( layerData_[i][j].prefabData.Value );
					gameObj.transform.position = PositionCalcUtil.mapIndexToVector3( new IntegerPair( j, i ) );
					addObject( gameObj.GetComponent<AbstractGameObject>() );
				}
			}
		}

		_size = new IntegerPair( layerData_.Length, layerData_[0].Length );
	}

	public IntegerPair layerSize
	{
		get {
			return _size;
		}
	}

	public bool isObjectExistAt( int x_, int y_ )
	{
		if (_obstacleHashmap [x_] != null) {
			return _obstacleHashmap [x_] [y_] != null;
		} else {
			return false;
		}
		
	}

	public AbstractGameObject getObjectAt( int x_, int y_ )
	{
		if (isObjectExistAt (x_, y_)) {
			return _obstacleHashmap[x_] [y_];
		}
		return null;
	}

	public override AbstractGameObject addObject (AbstractGameObject obj_)
	{
		if (!obj_.isObstacle) {
			_nonObstacleObjects.Add (obj_);
		} else {
			IntegerPair idxPair = obj_.positionIndexPair;
			_obstacleHashmap[ idxPair.x ][ idxPair.y ] = obj_;
		}
		return base.addObject (obj_);
	}

	public override AbstractGameObject removeObject (AbstractGameObject obj_)
	{
		IntegerPair idxPair;

		if (obj_.isObstacle) {
			idxPair = obj_.positionIndexPair;

			_obstacleHashmap [idxPair.x] [idxPair.y] = null;
		}

		return base.removeObject (obj_);
	}

	public override void moveObject (AbstractGameObject obj_, IntegerPair dst_)
	{
		if (obj_.isObstacle) {
			IntegerPair idxPair = obj_.positionIndexPair;
			_obstacleHashmap[ idxPair.x ][ idxPair.y ] = null;
			_obstacleHashmap[ dst_.x ][ dst_.y ] = obj_;
		}
	}
}
