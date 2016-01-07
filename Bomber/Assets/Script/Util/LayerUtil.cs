using UnityEngine;
using System.Collections;

namespace Boomscape.Util
{
    public class LayerUtil
    {
        static public int getLayerValue(params int[] layers)
        {
            int ret = 0;
            for (int i = 0; i < layers.Length; ++i)
            {
                ret += (1 << layers[i]);
            }

            return ret;
        }

        static public int clickableLayer
        {
            get
            {
                return getLayerValue(8, 9);
            }
        }

        static public int sightBlockLayer
        {
            get
            {
                return getLayerValue(10);
            }
        }
    }
}