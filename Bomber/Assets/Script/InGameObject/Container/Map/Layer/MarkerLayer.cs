using UnityEngine;
using System.Collections;
using Boomscape.Util;
using Boomscape.InGameObject.Block;
using Boomscape.Data.ValueObject.Game.InGameObject.Block;
using Boomscape.Util.Factory;
using Boomscape.Data.DataManager;

namespace Boomscape.InGameObject.Container.Map.Layer
{
    public class MarkerLayer : AbstractLayer
    {

        private IntegerPair _selectedPosition;

        private MarkerBlock.MarkerType[][] _backbuffer;

        // Use this for initialization
        void Start()
        {
            initObject();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void loadEmptyLayer(IntegerPair size_)
        {
            base.loadEmptyLayer(size_);

            resetBackbuffer();
        }

        public void resetBackbuffer()
        {
            _backbuffer = new MarkerBlock.MarkerType[layerSize.x][];

            for (int i = 0; i < layerSize.x; ++i)
            {
                _backbuffer[i] = new MarkerBlock.MarkerType[layerSize.y];
            }
        }

        public void markThrowableArea(IntegerPair position_, int radius_)
        {
            int i, j;
            int minX = Mathf.Max(position_.x - radius_, 0);
            int maxX = Mathf.Min(position_.x + radius_, layerSize.x - 1);
            int minY;
            int maxY;

            for (i = minX; i <= maxX; ++i)
            {
                minY = Mathf.Max(position_.y - (radius_ - Mathf.Abs(position_.x - i)), 0);
                maxY = Mathf.Min(position_.y + (radius_ - Mathf.Abs(position_.x - i)), layerSize.y - 1);
                for (j = minY; j <= maxY; ++j)
                {
                    if (GameMap.getInstance().isThrowable(i, j))
                    {
                        _backbuffer[i][j] = MarkerBlock.MarkerType.THROW_RANGE;
                    }
                }
            }
        }

        public void markExplosionArea(IntegerPair position_, bool[][] area_, IntegerPair positionInArea_)
        {
            _selectedPosition = position_.clone();
            int i, j;

            IntegerPair leftTop = new IntegerPair(
                                                  Mathf.Max(position_.x - positionInArea_.x, 0),
                                                  Mathf.Max(position_.y - positionInArea_.y, 0));
            IntegerPair rightBottom = new IntegerPair(
                                                      Mathf.Min(position_.x - positionInArea_.x + area_.Length, layerSize.x - 1),
                                                      Mathf.Min(position_.y - positionInArea_.y + area_[0].Length, layerSize.y - 1));

            for (i = leftTop.x; i < rightBottom.x; ++i)
            {
                for (j = leftTop.y; j < rightBottom.y; ++j)
                {
                    if (area_[i - leftTop.x][j - leftTop.y])
                    {
                        _backbuffer[i][j] = MarkerBlock.MarkerType.EXPLOSION_RANGE;
                    }
                }
            }

            _backbuffer[position_.x][position_.y] = MarkerBlock.MarkerType.BOMB_POSITION;
        }

        private void addMarker(int x_, int y_, MarkerBlock.MarkerType type_, Color color_)
        {
            BlockValueObject markerBlock = BlockDataManager.getInstance().findBlockData("SYSBLOCK0000");

            MarkerBlock newMarker;
            Vector3 pos;

            newMarker = GameObjectFactory.getInstance().generateObject(markerBlock.prefabData.Value).GetComponent<MarkerBlock>();
            pos = PositionCalcUtil.mapIndexToVector3(new IntegerPair(x_, y_));

            switch (type_)
            {
                case MarkerBlock.MarkerType.EXPLOSION_RANGE:
                    pos.z -= 1f;
                    break;
                case MarkerBlock.MarkerType.BOMB_POSITION:
                    pos.z -= 2f;
                    break;
            }
            newMarker.gameObject.GetComponentInChildren<Renderer>().material.color = color_;
            newMarker.transform.position = pos;
            newMarker.isTouchable = false;

            newMarker.markerType = type_;
            addObject(newMarker);
        }

        public void drawBackbuffer()
        {
            int i, j;
            MarkerBlock obj;

            for (i = 0; i < _backbuffer.Length; ++i)
            {
                for (j = 0; j < _backbuffer[i].Length; ++j)
                {
                    obj = getObjectAt(i, j) as MarkerBlock;
                    switch (_backbuffer[i][j])
                    {
                        case MarkerBlock.MarkerType.EXPLOSION_RANGE:
                            if (obj != null)
                            {
                                if (obj.markerType != MarkerBlock.MarkerType.EXPLOSION_RANGE)
                                {
                                    obj.destroyObject();

                                    addMarker(i, j, MarkerBlock.MarkerType.EXPLOSION_RANGE, new Color(1f, 0, 0, 0.3f));
                                }
                            }
                            else
                            {
                                addMarker(i, j, MarkerBlock.MarkerType.EXPLOSION_RANGE, new Color(1f, 0, 0, 0.3f));
                            }
                            break;
                        case MarkerBlock.MarkerType.THROW_RANGE:
                            if (obj != null)
                            {
                                if (obj.markerType != MarkerBlock.MarkerType.THROW_RANGE)
                                {
                                    obj.destroyObject();

                                    addMarker(i, j, MarkerBlock.MarkerType.THROW_RANGE, new Color(0, 1f, 0, 0.3f));
                                }
                            }
                            else
                            {
                                addMarker(i, j, MarkerBlock.MarkerType.THROW_RANGE, new Color(0, 1f, 0, 0.3f));
                            }
                            break;
                        case MarkerBlock.MarkerType.BOMB_POSITION:
                            if (obj != null)
                            {
                                if (obj.markerType != MarkerBlock.MarkerType.BOMB_POSITION)
                                {
                                    obj.destroyObject();

                                    addMarker(i, j, MarkerBlock.MarkerType.BOMB_POSITION, new Color(0, 0, 1f, 0.3f));
                                }
                            }
                            else
                            {
                                addMarker(i, j, MarkerBlock.MarkerType.BOMB_POSITION, new Color(0, 0, 1f, 0.3f));
                            }
                            break;
                        default:
                            if (obj != null)
                            {
                                obj.destroyObject();
                            }
                            break;
                    }
                }
            }
        }

        public override AbstractGameObject removeObject(AbstractGameObject obj_)
        {
            IntegerPair pos = obj_.positionIndexPair;
            _backbuffer[pos.x][pos.y] = MarkerBlock.MarkerType.NONE;
            return base.removeObject(obj_);
        }

        public override void removeAll(bool destroyFlag)
        {
            base.removeAll(destroyFlag);

            _selectedPosition = null;
            resetBackbuffer();
        }

        public bool isSelectedBlock(IntegerPair position_)
        {
            return _selectedPosition != null && position_.ToString() == _selectedPosition.ToString();
        }

        public IntegerPair getSelectedPos()
        {
            return _selectedPosition;
        }

        public void resetSelectedPos()
        {
            _selectedPosition = null;
        }
    }
}