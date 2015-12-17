using UnityEngine;
using System.Collections;
using System;

public class EnemyWatchmanCharacter : AbstractCharacter {

    public enum WatchmanState { WATCHING, CHASING };

    private bool _enemyInitFlag = false;
    private WatchmanState _state;
    private AbstractGameObject _chaseTarget;
    private IntegerPair _initPos;

    [Range( 1, 10 )]
    public int ViewRange;

	// Use this for initialization
	void Start () {
        initObject();
	}
	
	// Update is called once per frame
	void Update () {
        updateObject();
	}

    protected override void initObject()
    {
        if (!_enemyInitFlag)
        {
            _enemyInitFlag = true;

            _state = WatchmanState.WATCHING;
            _initPos = positionIndexPair.clone();
        }
    }

    protected override void updateObject()
    {
        base.updateObject();

        switch (_state)
        {
            case WatchmanState.WATCHING:
                Vector2 pos = transform.position;
                RaycastHit2D[] hits = new RaycastHit2D[4];
                hits[0] = Physics2D.Raycast(pos, new Vector2( -1, 0 ), ViewRange * GameMapConst.BLOCK_SIZE, LayerUtil.sightBlockLayer);
                hits[1] = Physics2D.Raycast(pos, new Vector2(1, 0), ViewRange * GameMapConst.BLOCK_SIZE, LayerUtil.sightBlockLayer);
                hits[2] = Physics2D.Raycast(pos, new Vector2(0, 1), ViewRange * GameMapConst.BLOCK_SIZE, LayerUtil.sightBlockLayer);
                hits[3] = Physics2D.Raycast(pos, new Vector2(0, -1), ViewRange * GameMapConst.BLOCK_SIZE, LayerUtil.sightBlockLayer);

                PlayerCharacter player;

                for (int i = 0; i < hits.Length; ++i)
                {
                    if (hits[i].collider != null)
                    {
                        player = hits[i].collider.gameObject.GetComponentInParent<PlayerCharacter>();
                        if (player != null)
                        {
                            Debug.Log("player found : now chase");
                            _chaseTarget = player;
                            setPath(PathFinder.findPath(GameMap.getInstance(), positionIndexPair, _chaseTarget.positionIndexPair));
                            _state = WatchmanState.CHASING;
                            break;
                        }
                    }
                }

                break;
            case WatchmanState.CHASING:
                if (PositionCalcUtil.tileRectFromIdxPair(positionIndexPair).Overlaps(PositionCalcUtil.tileRectFromIdxPair(_chaseTarget.positionIndexPair)))
                {
                    _chaseTarget.destroyObject();

                    backToWork();
                }
                else if( positionIndexPair.sub( _chaseTarget.positionIndexPair).xySum > 10 )
                {
                    
                }
                else if (!isPathExist)
                {
                    setPath(PathFinder.findPath(GameMap.getInstance(), positionIndexPair, _chaseTarget.positionIndexPair));
                }
                break;
            default:
                break;
        }
    }

    private void backToWork()
    {
        _chaseTarget = null;
        setPath(PathFinder.findPath(GameMap.getInstance(), positionIndexPair, _initPos));
        _state = WatchmanState.WATCHING;
    }

    public override void onExplosion(AbstractBombValueObject bombData_)
    {
        
    }

    public override float speed
    {
        get
        {
            return 3;
        }
    }
}
