using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathHeap
{
    private List<AStarPath> pathHeap;
    private IntegerPair dest;

    public PathHeap(IntegerPair dest_)
    {
        pathHeap = new List<AStarPath>();
        this.dest = dest_;
    }

    public void insert(AStarPath newNode_)
    {
        pathHeap.Add(newNode_);

        if (pathHeap.Count > 1)
        {
            upHeap(pathHeap.Count - 1);
        }
    }

    private void upHeap(int idx_)
    {
        if (idx_ != 0)
        {
            int parentIdx = getParentIdx(idx_);

            if (heuristicCost(pathHeap[parentIdx], dest) > heuristicCost(pathHeap[idx_], dest))
            {
                swap(parentIdx, idx_);
                upHeap(parentIdx);
            }
        }

        return;
    }

    public int heapSize
    {
        get
        {
            return pathHeap.Count;
        }
    }

    public AStarPath extract()
    {
        AStarPath retValue = pathHeap[0];

        if (pathHeap.Count > 1)
        {
            AStarPath newRoot = pathHeap[pathHeap.Count - 1];

            pathHeap[0] = newRoot;
            pathHeap.RemoveAt(pathHeap.Count - 1);
            downHeap(0);
        }
        else if (pathHeap.Count == 1)
        {
            pathHeap.RemoveAt(0);
        }

        return retValue;
    }

    private void downHeap(int idx_)
    {
        int lChildIdx = getLeftChildIdx(idx_);
        int rChildIdx = getRightChildIdx(idx_);
        if (lChildIdx >= pathHeap.Count)
        { // no children
            return;
        }

        if (rChildIdx >= pathHeap.Count || heuristicCost(pathHeap[lChildIdx], dest) < heuristicCost(pathHeap[rChildIdx], dest))
        {       // no right children
            if (heuristicCost(pathHeap[lChildIdx], dest) < heuristicCost(pathHeap[idx_], dest))
            {
                swap(lChildIdx, idx_);
                downHeap(lChildIdx);
            }
        }
        else
        {
            if (heuristicCost(pathHeap[rChildIdx], dest) < heuristicCost(pathHeap[idx_], dest))
            {
                swap(rChildIdx, idx_);
                downHeap(rChildIdx);
            }
        }
    }

    private void swap(int idx1_, int idx2_)
    {
        AStarPath tmp = pathHeap[idx1_];
        pathHeap[idx1_] = pathHeap[idx2_];
        pathHeap[idx2_] = tmp;
    }

    private int getParentIdx(int idx_)
    {
        return Mathf.FloorToInt(idx_ - 1 / 2);
    }

    private int getLeftChildIdx(int idx_)
    {
        return idx_ * 2 + 1;
    }

    private int getRightChildIdx(int idx_)
    {
        return idx_ * 2 + 2;
    }

    private static float heuristicCost(IntegerPair p1_, IntegerPair p2_)
    {
        return p1_.distance(p2_);
    }


}
