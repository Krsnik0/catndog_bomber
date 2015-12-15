using UnityEngine;
using System.Collections;

public class AStarPath : IntegerPair {

	public float cost { get; set; }
	public AStarPath header { get; set; }

	public AStarPath( int x_, int y_ ) : base( x_, y_ )
	{
	}

	public AStarPath( IntegerPair point_ ) : base( point_.x, point_.y )
	{
	}
}
