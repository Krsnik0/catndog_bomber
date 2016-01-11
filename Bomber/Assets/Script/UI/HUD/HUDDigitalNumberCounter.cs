using UnityEngine;

namespace Boomscape.UI.HUD
{

    class HUDDigitalNumberCounter : AbstractUI
    {
        private bool _initNumberCounterFlag = false;
        private int _number;
        private Animator _numberSprite;

        private void Start()
        {
            initObject();
        }

        private void Update()
        {
            updateObject();
        }

        public void setInteger( int number_ )
        {
            int clippedNumber = Mathf.Clamp(number_, 0, 9);
            _numberSprite.Play("DigitalNum" + clippedNumber.ToString());
        }

        protected override void initObject()
        {
            base.initObject();

            if( !_initNumberCounterFlag )
            {
                _initNumberCounterFlag = true;

                _numberSprite = GetComponent<Animator>();

                _numberSprite.Play("DigitalNum0");
            }
        }

        protected override void updateObject()
        {
        }
    }
}
