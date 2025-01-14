using System;
using Game.Barrels.DataSos;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Barrels
{
    [Serializable]
    public class ObjectAndCountPair<T> where T: BarrelDataSo
    {
        [field: SerializeField]
        public T BarrelDataSo { get; private set; }
        
        [SerializeField]
        private Vector2 healthPoints;

        public int GetRandomHealth => Random.Range((int)healthPoints.x, (int)healthPoints.y);
    }
}