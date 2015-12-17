using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder {

	public static List<AStarPath> findPath( GameMap map_, IntegerPair src_, IntegerPair dst_ )
	{
		if (!map_.isMovable (dst_.x, dst_.y)) {
			return null;
		}

		PathHeap openList = new PathHeap (dst_);
		Dictionary<string, AStarPath> closedList = new Dictionary<string, AStarPath>();

		AStarPath srcPoint = new AStarPath (src_);
		srcPoint.cost = 0;
		srcPoint.header = null;
		openList.insert (srcPoint);

		AStarPath currentPoint;
		AStarPath dstPoint = null;

		int i;

		do {
			currentPoint = openList.extract();
            //Debug.Log(currentPoint + "," + openList.heapSize);

            if ( currentPoint.ToString() == dst_.ToString() )
			{
				dstPoint = currentPoint;
				break;
			}
			else
			{
				closedList.Add( currentPoint.ToString(), currentPoint );
				AStarPath[] adjacents = new AStarPath[4];

				adjacents[0] = new AStarPath( currentPoint.x + 1, currentPoint.y );
				adjacents[1] = new AStarPath( currentPoint.x - 1, currentPoint.y );
				adjacents[2] = new AStarPath( currentPoint.x, currentPoint.y + 1 );
				adjacents[3] = new AStarPath( currentPoint.x, currentPoint.y - 1 );

				for( i = 0; i < adjacents.Length; ++ i )
				{
					if( map_.isMovable( adjacents[i].x, adjacents[i].y ) )
					{
						adjacents[i].cost = currentPoint.cost + 1;
						adjacents[i].header = currentPoint;
						if( closedList.ContainsKey( adjacents[i].ToString() ) )
						{
							if( closedList[ adjacents[i].ToString() ].cost > adjacents[i].cost )
							{
								closedList[ adjacents[i].ToString() ] = adjacents[i];
							}
						}
						else
						{
							openList.insert( adjacents[i] );
						}
					}
				}
			}

		} while( openList.heapSize > 0 );

		if (dstPoint != null) {
			Debug.Log( "path found : from " + src_.ToString() + " to " + dst_.ToString() );
			List<AStarPath> path = new List<AStarPath> ();
			while (dstPoint != null) {
				path.Add (dstPoint);
				dstPoint = dstPoint.header;
			}

			path.Reverse();
			return path;
		} else {
			return null;
		}
	}

}
