using System;
using Game.Barrels.DataSos;
using UnityEngine;

namespace Game.Barrels
{
    [Serializable]
    public class ObjectAndCountPair_Default: ObjectAndCountPair<BarrelDataSo_Default>
    {
        [field: SerializeField]
        public int HowManyObject { get; private set; }
    }
}