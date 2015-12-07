using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionCalcUtil {
    static public IntegerPair vector3ToMapIndex(Vector3 position_)
    {
        return new IntegerPair(
            Mathf.FloorToInt(position_.x / GameMapConst.BLOCK_SIZE),
            Mathf.FloorToInt(position_.y / GameMapConst.BLOCK_SIZE)
            );
    }

    static public Vector3 mapIndexToVector3( IntegerPair idxPair_ )
    {
        return new Vector3((idxPair_.x) * GameMapConst.BLOCK_SIZE, (idxPair_.y) * GameMapConst.BLOCK_SIZE, idxPair_.x + idxPair_.y);
    }

	static public Rect tileRectFromIdxPair( IntegerPair idxPair_ )
	{
		Vector3 pos = mapIndexToVector3 (idxPair_);

		return new Rect (pos.x, pos.y, GameMapConst.BLOCK_SIZE, GameMapConst.BLOCK_SIZE);
	}
}
