using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boomscape.Data.Constant
{
    class UIPrefabConst
    {
        public const string HUD_BOMB_COUNTER_PATH = "Prefab/UI/HUD/Counter";
        static public KeyValuePair<Type, string> HUD_BOMB_COUNTER
        {
            get
            {
                return new KeyValuePair<Type, string>(typeof(GameObject), HUD_BOMB_COUNTER_PATH);
            }
        }
    }
}
