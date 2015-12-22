using UnityEngine;
using System.Collections;

public class StageValueObject : AbstractValueObject {

	public AbstractGameObjectValueObject[][] objLayer { get; set; }
	public AbstractGameObjectValueObject[][] tileLayer { get; set; }
	public AbstractBombValueObject[] allowedBombs { get; set; }

	public IntegerPair entryPoint { get; set; }
    public IntegerPair goal { get; set; }
    public IntegerPair mapSize { get; set; }

	public StageValueObject()
	{
	}

	public override AbstractValueObject clone ()
	{
		StageValueObject ret = new StageValueObject ();
		ret.objLayer = this.objLayer;
		ret.tileLayer = this.tileLayer;
		ret.entryPoint = this.entryPoint;
        ret.goal = this.goal;
        ret.mapSize = this.mapSize;

		return ret;
	}
}
