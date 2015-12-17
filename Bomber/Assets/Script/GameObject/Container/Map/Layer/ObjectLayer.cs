using UnityEngine;
using System.Collections;

public class ObjectLayer : AbstractLayer {

	private bool _objLayerInitFlag = false;
	private PlayerCharacter _playerCharacter;

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void initObject ()
	{
		base.initObject ();

		if (!_objLayerInitFlag) {
			_objLayerInitFlag = true;

			EventManager.getInstance().addEventListener( BombExplodeEvent.BOMB_EXPLODE_EVENT_KEY, onBombExploded );
		}
	}

	public override void loadLayer (AbstractGameObjectValueObject[][] layerData_)
	{
		base.loadLayer (layerData_);

		_playerCharacter = GetComponentInChildren<PlayerCharacter> ();
	}

	public PlayerCharacter playerCharacter
	{
		get {
			return _playerCharacter;
		}
	}

	public void explode( AbstractBomb bomb_ )
	{
		int i, j, k;
		IntegerPair bombPos = bomb_.positionIndexPair;
		IntegerPair bombCenter = bomb_.bombData.bombPosition;
		IntegerPair currentEffectPos;

        bool[][] explosionShape = bomb_.bombData.explosionShape;

        FlameBlock flameBlock;

		for( i = 0; i < explosionShape.Length; ++ i )
		{
			for( j = 0; j < explosionShape[i].Length; ++ j )
			{
				if( explosionShape[i][j] )
				{
					currentEffectPos = bombPos.sub( bombCenter ).add( new IntegerPair( i, j ) );

                    if (0 <= currentEffectPos.x && currentEffectPos.x < layerSize.x &&
                       0 <= currentEffectPos.y && currentEffectPos.y < layerSize.y)
                    {
                        if (isObjectExistAt(currentEffectPos.x, currentEffectPos.y))
                        {
                            getObjectAt(currentEffectPos.x, currentEffectPos.y).onExplosion(bomb_.bombData);
                        }

                        flameBlock = GameObjectFactory.getInstance().generateObject(BlockDataManager.getInstance().findBlockData("SYSBLOCK0001").prefabData.Value).GetComponent<FlameBlock>();
                        flameBlock.positionIndexPair = currentEffectPos.clone();
                        flameBlock.transform.parent = transform;        // WARNING: must not add to layerHashmap. May overwrite not destructed block.

                        for (k = _nonObstacleObjects.Count - 1; k >= 0; --k)
                        {
                            if (PositionCalcUtil.tileRectFromIdxPair(_nonObstacleObjects[k].positionIndexPair).Overlaps(
                                PositionCalcUtil.tileRectFromIdxPair(currentEffectPos))
                               )
                            {
                                _nonObstacleObjects[k].onExplosion(bomb_.bombData);
                            }
                        }
                    }
				}
			}
		}
	}

	private void onBombExploded( AbstractEvent event_ )
	{
		BombExplodeEvent bombEvent = event_ as BombExplodeEvent;
		
		if (bombEvent.targetGameObject != null) {
			explode( bombEvent.targetGameObject );
		}
	}
}
