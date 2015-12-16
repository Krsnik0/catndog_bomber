using UnityEngine;
using System.Collections;

/*
1 = explosion point
0 = empty point
B = Bomb place

1 1 1
1 B 1
1 1 1
*/

public class BombType1 : AbstractBombValueObject {

    public BombType1(string code_, int throwRadius_, int explosionTime_, int minSize_, int maxSize_ ) : base(code_, throwRadius_, explosionTime_, minSize_, maxSize_)
    {
    }

    public BombType1( Object rawData_ ) : base( rawData_ )
    {
    }

    public override AbstractValueObject clone()
    {
        return new BombType1(rawData);
    }

    public override bool[][] explosionShape
    {
        get
        {
            int clippedSize = calcClippedSize( currentSize );
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
