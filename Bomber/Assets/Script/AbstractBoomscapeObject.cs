using UnityEngine;
using System.Collections;

namespace Boomscape
{
    public abstract class AbstractBoomscapeObject : MonoBehaviour
    {

        abstract protected void initObject();
        abstract protected void updateObject();
    }
}