using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Boomscape.Data.ValueObject.Game.InGameObject
{
    public abstract class AbstractGameObjectValueObject : AbstractValueObject
    {

        abstract public string code { get; }
        abstract public KeyValuePair<Type, string> prefabData { get; }
    }
}