using UnityEngine;
using System.Collections;

namespace Boomscape.Data.DataManager
{
    public abstract class AbstractDataManager
    {
        abstract public void loadData();
        abstract public void dispose();
    }
}
