﻿using UnityEngine;
using System.Collections;
using Boomscape.Util;

namespace Boomscape.Data.ValueObject.Game.InGameObject.Bomb
{
    public class BombType1 : AbstractBombValueObject
    {

        public BombType1(string code_, int throwRadius_, int explosionTime_, int minSize_, int maxSize_) : base(code_, throwRadius_, explosionTime_, minSize_, maxSize_)
        {
        }

        public BombType1(object rawData_) : base(rawData_)
        {
        }

        public override AbstractValueObject clone()
        {
            BombType1 ret = new BombType1(code, throwRange, explosionTime, minSize, maxSize);
            ret.currentSize = currentSize;
            return ret;
        }

        public override bool[][] explosionShape
        {
            get
            {
                int clippedSize = calcClippedSize(currentSize);
                int sideLen = 2 * clippedSize + 1;

                bool[][] retValue = new bool[sideLen][];

                int i, j;

                for (i = 0; i < sideLen; ++i)
                {
                    retValue[i] = new bool[sideLen];
                    for (j = 0; j < sideLen; ++j)
                    {
                        retValue[i][j] = true;
                    }
                }

                return retValue;
            }
        }

        public override IntegerPair bombPosition
        {
            get
            {
                int clippedSize = calcClippedSize(currentSize);

                return new IntegerPair(clippedSize, clippedSize);
            }
        }
    }
}