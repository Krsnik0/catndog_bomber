using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomscape.Util.PathFinding;
using Boomscape.Util;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Manager;
using Boomscape.Data.Event.Request;
using Boomscape.Data.Event.GameObject;

namespace Boomscape.InGameObject.Character
{
    public abstract class AbstractCharacter : AbstractGameObject
    {

        private List<AStarPath> _path;
        private IntegerPair _prevPathElement;

        public override bool isObstacle
        {
            get
            {
                return false;
            }
        }

        public override GameMapLayer layer
        {
            get
            {
                return GameMapLayer.OBJECT;
            }
        }

        public abstract float speed { get; }
        

        public void setPath(List<AStarPath> path)
        {
            _prevPathElement = positionIndexPair;

            this._path = path;
        }

        protected bool isPathExist
        {
            get
            {
                return _path != null;
            }
        }

        protected override void updateObject()
        {
            if (_path != null)
            {
                if (GameManager.getInstance().currentState == GameState.THROWING)
                {
                    EventManager.getInstance().dispatchEvent(new UpdateRequestEvent(typeof(GameMap)));
                }
                if (GameMap.getInstance().isMovable(_path[0].x, _path[0].y))
                {
                    Vector3 dst = PositionCalcUtil.mapIndexToVector3(_path[0]);
                    Vector3 delta = (dst - transform.position).normalized * speed * Time.deltaTime;
                    transform.position += delta;

                    if (Vector3.Distance(dst, transform.position) < 0.01f * speed * Time.timeScale)
                    {
                        transform.position = dst;
                        _prevPathElement = _path[0];
                        _path.RemoveAt(0);

                        if (_path.Count == 0)
                        {
                            Debug.Log("arrived");
                            _path = null;
                        }
                    }
                }
                else
                {
                    onBlockedWhileMoving();
                }
            }
        }

        virtual protected void onBlockedWhileMoving()
        {
            positionIndexPair = _prevPathElement;
            _path = null;
        }

        public override void destroyObject()
        {
            EventManager.getInstance().dispatchEvent(new ObjectRemovedEvent(this));
            Destroy(gameObject);
        }
    }
}