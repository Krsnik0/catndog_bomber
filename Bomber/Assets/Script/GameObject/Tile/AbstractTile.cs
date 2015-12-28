using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractTile : AbstractGameObject {

    public override void onExplosion(AbstractBombValueObject bombData_)
    {
    }

    public override bool isObstacle
    {
        get
        {
            return false;
        }
    }

    public override GameMap.GameMapLayer layer
    {
        get
        {
            return GameMap.GameMapLayer.TILE;
        }
    }

    public override void destroyObject()
    {
        Destroy(gameObject);
    }
}
