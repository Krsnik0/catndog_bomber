using UnityEngine;
using System.Collections;

public class IntegerPair {
	public int x { get; set; }
	public int y { get; set; }

	public IntegerPair( int x_, int y_ )
	{
		this.x = x_;
		this.y = y_;
	}

	public IntegerPair clone()
	{
		return new IntegerPair( x, y );
	}

	public IntegerPair add( IntegerPair operand_ )
	{
		return new IntegerPair (x + operand_.x, y + operand_.y);
	}

	public IntegerPair multiply( int operand_ )
	{
		return new IntegerPair (x * operand_, y * operand_);
	}

	public IntegerPair sub( IntegerPair operand_ )
	{
		return clone().add( operand_.multiply( -1 ) );
	}

	public float distance( IntegerPair point_ )
	{
		return Mathf.Sqrt ((point_.x - x) * (point_.x - x) + (point_.y - y) * (point_.y - y));
	}

	public override string ToString ()
	{
		return string.Format ("({0}, {1})", x, y);
	}

    public int xySum
    {
        get
        {
            return x + y;
        }
    }
}
