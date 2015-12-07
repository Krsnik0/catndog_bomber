using UnityEngine;
using System.Collections;

public class StageValueObject : AbstractValueObject {

	public AbstractGameObjectValueObject[][] objLayer { get; set; }
	public AbstractGameObjectValueObject[][] tileLayer { get; set; }
	public BombValueObject[] allowedBombs { get; set; }

	public IntegerPair entryPoint { get; set; }

	public StageValueObject()
	{
	}

	public override AbstractValueObject clone ()
	{
		StageValueObject ret = new StageValueObject ();
		ret.objLayer = this.objLayer;
		ret.tileLayer = this.tileLayer;
		ret.entryPoint = this.entryPoint;

		return ret;
	}
}
