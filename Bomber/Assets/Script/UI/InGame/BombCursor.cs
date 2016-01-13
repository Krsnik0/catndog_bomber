using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Boomscape.UI.InGame
{
    public class BombCursor : AbstractUI
    {

        private bool _bombCursorInitFlag = false;

        // Use this for initialization
        void Start()
        {
            initObject();
        }

        // Update is called once per frame
        void Update()
        {
            updateObject();
        }

        protected override void initObject()
        {
            if (!_bombCursorInitFlag)
            {
                _bombCursorInitFlag = true;
            }
        }

        protected override void updateObject()
        {

        }

        public override void onChildClick(AbstractUI child_)
        {
            base.onChildClick(child_);
        }
    }
}