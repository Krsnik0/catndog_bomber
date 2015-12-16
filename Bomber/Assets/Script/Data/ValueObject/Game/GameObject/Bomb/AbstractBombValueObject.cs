using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractBombValueObject : AbstractGameObjectValueObject {

	private string _bombCode;

	private int _throwRange;
	private int _explosionTime;
    private int _minSize;
    private int _maxSize;
    private float _currentSize;
	
	public AbstractBombValueObject( string code_, int throwRadius_, int explosionTime_, int minSize_, int maxSize_ )
	{
		this._bombCode = code_;
		this._throwRange = throwRadius_;
		this._explosionTime = explosionTime_;
        this._minSize = minSize_;
        this._maxSize = maxSize_;

        _currentSize = minSize;
	}
	
	protected AbstractBombValueObject( Object rawData_ )
	{
	}

    public abstract bool[][] explosionShape { get; }
    public abstract IntegerPair bombPosition { get; }

    protected int calcClippedSize(float size_)
    {
        return Mathf.Min(Mathf.Max( Mathf.RoundToInt(size_), minSize), maxSize); ;
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

    public int minSize
    {
        get
        {
            return _minSize;
        }
    }

    public int maxSize
    {
        get
        {
            return _maxSize;
        }
    }

    public float currentSize
    {
        get
        {
            return _currentSize;
        }

        set
        {
            _currentSize = calcClippedSize( value );
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
