using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombValueObject : AbstractGameObjectValueObject {

	private string _bombCode;

	private bool[][] _explosionShape;
	private IntegerPair _bombPos;

	private int _throwRange;
	private int _explosionTime;
	
	public BombValueObject( string code_, bool[][] explosionShape_, IntegerPair bombPos_, int throwRadius_, int explosionTime_ )
	{
		this._bombCode = code_;
		this._explosionShape = explosionShape_;
		this._bombPos = bombPos_;
		this._throwRange = throwRadius_;
		this._explosionTime = explosionTime_;
	}
	
	private BombValueObject( Object rawData_ )
	{
	}
	
	public override AbstractValueObject clone ()
	{
		return new BombValueObject (rawData);
	}

	public bool[][] explosionShape
	{
		get
		{
			return _explosionShape;
		}
	}

	public IntegerPair bombPosition
	{
		get
		{
			return _bombPos;
		}
	}

	public int throwRange
	{
		get
		{
			return _throwRange;
		}
	}

	public int explosionTime
	{
		get
		{
			return _explosionTime;
		}
	}
	
	public override string code {
		get {
			return _bombCode;
		}
	}
	
	public override KeyValuePair<System.Type, string> prefabData {
		get {
			return new KeyValuePair<System.Type, string>( typeof(GameObject), "Prefab/Bomb/" + code );
		}
	}

	public KeyValuePair<System.Type, string> iconPath {
		get {
			return new KeyValuePair<System.Type, string>( typeof(GameObject), "Prefab/UI/Icon/Bomb/" + code );
		}
	}

}
