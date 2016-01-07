using UnityEngine;
using System.Collections;
using System;

namespace Boomscape.InGameObject.Tile
{
    public class EntryTile : AbstractTile
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