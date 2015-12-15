using UnityEngine;
using System.Collections;

public abstract class AbstractGameObject : AbstractBoomscapeObject {

	public IntegerPair positionIndexPair
	{
		get
		{
			return PositionCalcUtil.vector3ToMapIndex (transform.position);
		}

		set
		{
			transform.position = PositionCalcUtil.mapIndexToVector3( value );
		}
	}

	public abstract bool isObstacle { get; }
	public abstract GameMap.GameMapLayer layer { get; }

	public abstract void onExplosion( BombValueObject bombData_ );
	public abstract void destroyObject ();
}
