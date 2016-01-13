using UnityEngine;
using System.Collections;

namespace Boomscape.InGameObject.Tile
{
    public class GoalTile : AbstractTile
    {

        private void Start()
        {
            initObject();
        }

        private void Update()
        {
            updateObject();
        }

        protected override void initObject()
        {
        }

        protected override void updateObject()
        {
        }
    }
}