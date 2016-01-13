using UnityEngine;
using System.Collections.Generic;
using System;
using Boomscape.Util;
using Boomscape.InGameObject.Bomb;
using Boomscape.Manager;
using Boomscape.Data.Event.GameObject;
using Boomscape.Data.Event;
using Boomscape.Data.Constant;
using Boomscape.Util.PathFinding;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;

namespace Boomscape.InGameObject.Character
{
    public class EnemyWatchmanCharacter : AbstractCharacter
    {

        public enum WatchmanState { WATCHING, CHASING, RUN_AWAY };

        private bool _enemyInitFlag = false;
        private WatchmanState _state;
        private AbstractGameObject _chaseTarget;
        private IntegerPair _initPos;
        private List<AbstractBomb> _foundBombs;

        [Range(1, 10)]
        public int ViewRange;

        // Use this for initialization
        void Start()
        {
            initObject();
        }

        // Update is called once per frame
        void Update()
        {
            updateObject();
        }

        protected override void initObject()
        {
            if (!_enemyInitFlag)
            {
                _enemyInitFlag = true;

                _state = WatchmanState.WATCHING;
                _initPos = positionIndexPair.clone();
                _foundBombs = new List<AbstractBomb>();

                EventManager.getInstance().addEventListener(ObjectRemovedEvent.OBJECT_REMOVED_EVENT_KEY, onBombExplode);
            }
        }

        private void onBombExplode(AbstractEvent event_)
        {
            ObjectRemovedEvent objEvent = event_ as ObjectRemovedEvent;

            if (objEvent.targetGameObject is AbstractBomb)
            {
                AbstractBomb bomb = objEvent.targetGameObject as AbstractBomb;

                if (_foundBombs.Contains(bomb))
                {
                    _foundBombs.Remove(bomb);

                    if (_foundBombs.Count == 0)
                    {
                        backToWork();
                    }
                }
            }
        }

        protected override void updateObject()
        {
            base.updateObject();

            int i;
            Vector2 pos = transform.position;
            RaycastHit2D[] hits = new RaycastHit2D[4];
            AbstractGameObject foundout;
            AbstractBomb foundBomb;

            bool bombFound = false;

            hits[0] = Physics2D.Raycast(pos, new Vector2(-1, 0), ViewRange * GameMapConst.BLOCK_SIZE_WIDTH, LayerUtil.sightBlockLayer);
            hits[1] = Physics2D.Raycast(pos, new Vector2(1, 0), ViewRange * GameMapConst.BLOCK_SIZE_WIDTH, LayerUtil.sightBlockLayer);
            hits[2] = Physics2D.Raycast(pos, new Vector2(0, 1), ViewRange * GameMapConst.BLOCK_SIZE_HEIGHT, LayerUtil.sightBlockLayer);
            hits[3] = Physics2D.Raycast(pos, new Vector2(0, -1), ViewRange * GameMapConst.BLOCK_SIZE_HEIGHT, LayerUtil.sightBlockLayer);

            for (i = 0; i < hits.Length; ++i)
            {
                if (hits[i].collider != null)
                {
                    foundout = hits[i].collider.gameObject.GetComponentInParent<AbstractGameObject>();
                    foundBomb = foundout as AbstractBomb;
                    if (foundout is AbstractBomb && !_foundBombs.Contains(foundBomb))
                    {
                        Debug.Log("bomb found");
                        _foundBombs.Add(foundBomb);
                        bombFound = true;
                        break;
                    }
                }
            }

            switch (_state)
            {
                case WatchmanState.RUN_AWAY:
                    if (bombFound)
                    {
                        setPath(SafeZoneFinder.findSafeZone(GameMap.getInstance(), positionIndexPair));
                        _state = WatchmanState.RUN_AWAY;
                    }
                    break;
                case WatchmanState.WATCHING:
                    if (bombFound)
                    {
                        setPath(SafeZoneFinder.findSafeZone(GameMap.getInstance(), positionIndexPair));
                        _state = WatchmanState.RUN_AWAY;
                    }
                    else
                    {
                        for (i = 0; i < hits.Length; ++i)
                        {
                            if (hits[i].collider != null)
                            {
                                foundout = hits[i].collider.gameObject.GetComponentInParent<AbstractGameObject>();
                                if (foundout is PlayerCharacter)
                                {
                                    Debug.Log("player found : now chase");
                                    _chaseTarget = foundout;
                                    setPath(PathFinder.findPath(GameMap.getInstance(), positionIndexPair, _chaseTarget.positionIndexPair));
                                    _state = WatchmanState.CHASING;
                                    break;
                                }
                            }
                        }
                    }
                    break;
                case WatchmanState.CHASING:
                    if (bombFound)
                    {
                        setPath(SafeZoneFinder.findSafeZone(GameMap.getInstance(), positionIndexPair));
                        _state = WatchmanState.RUN_AWAY;
                    }
                    else
                    {
                        if (PositionCalcUtil.tileRectFromIdxPair(positionIndexPair).Overlaps(PositionCalcUtil.tileRectFromIdxPair(_chaseTarget.positionIndexPair)))
                        {
                            _chaseTarget.destroyObject();

                            backToWork();
                        }
                        else if (positionIndexPair.sub(_chaseTarget.positionIndexPair).xySum > 10)
                        {
                            backToWork();
                        }
                        else if (!isPathExist)
                        {
                            setPath(PathFinder.findPath(GameMap.getInstance(), positionIndexPair, _chaseTarget.positionIndexPair));
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void onBlockedWhileMoving()
        {
            Debug.Log("blocked");
            base.onBlockedWhileMoving();
            backToWork();
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
}