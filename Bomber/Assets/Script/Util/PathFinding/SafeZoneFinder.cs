using UnityEngine;
using System.Collections.Generic;

class SafeZoneFinder
{
    public static List<AStarPath> findSafeZone(GameMap map_, IntegerPair src_)
    {
        if ( map_.isSafe( src_.x, src_.y ) )
        {
            Debug.Log("already safe");
            return null;
        }

        Queue<AStarPath> queue = new Queue<AStarPath>();
        Dictionary<string, AStarPath> closedList = new Dictionary<string, AStarPath>();

        AStarPath srcPoint = new AStarPath(src_);
        srcPoint.cost = 0;
        srcPoint.header = null;
        queue.Enqueue(srcPoint);

        AStarPath currentPoint;
        AStarPath dstPoint = null;

        int i;

        do
        {
            currentPoint = queue.Dequeue();

            if (map_.isSafe(currentPoint.x, currentPoint.y))
            {
                dstPoint = currentPoint;
                break;
            }
            else
            {
                if (!closedList.ContainsKey(currentPoint.ToString()))
                {
                    closedList.Add(currentPoint.ToString(), currentPoint);
                    AStarPath[] adjacents = new AStarPath[4];

                    adjacents[0] = new AStarPath(currentPoint.x + 1, currentPoint.y);
                    adjacents[1] = new AStarPath(currentPoint.x - 1, currentPoint.y);
                    adjacents[2] = new AStarPath(currentPoint.x, currentPoint.y + 1);
                    adjacents[3] = new AStarPath(currentPoint.x, currentPoint.y - 1);

                    for (i = 0; i < adjacents.Length; ++i)
                    {
                        if (map_.isMovable(adjacents[i].x, adjacents[i].y))
                        {
                            adjacents[i].cost = currentPoint.cost + 1;
                            adjacents[i].header = currentPoint;
                            if (closedList.ContainsKey(adjacents[i].ToString()))
                            {
                                if (closedList[adjacents[i].ToString()].cost > adjacents[i].cost)
                                {
                                    closedList[adjacents[i].ToString()] = adjacents[i];
                                }
                            }
                            else
                            {
                                queue.Enqueue(adjacents[i]);
                            }
                        }
                    }
                }
            }

        } while (queue.Count > 0);

        if (dstPoint != null)
        {
            Debug.Log("safe zone found : " + dstPoint.ToString());
            List<AStarPath> path = new List<AStarPath>();
            while (dstPoint != null)
            {
                path.Add(dstPoint);
                dstPoint = dstPoint.header;
            }

            path.Reverse();
            return path;
        }
        else
        {
            return null;
        }
    }
}