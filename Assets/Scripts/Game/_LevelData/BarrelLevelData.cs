using System;
using Game.Barrels;
using Game.Barrels.DataSos;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game._LevelData
{
    [Serializable]
    public class BarrelLevelData
    {
        [field: SerializeField,
                GUIColor( 1, 0.5f, 0.3f, 1f),
                Tooltip("Boss data is specific to each level. Please override them from LevelDataBase object.")]
        public ObjectAndCountPair_Boss ObjectAndCountPairBoss { get; private set; }
        
        [field: SerializeField,
                GUIColor( 0.8f, 1, 0.5f, 1f),
                Space(20),
                InfoBox("BarrelSpawnDataSos can use from other LevelDataBase objects.So please don't override them from LevelDataBase object.")]
        public BarrelSpawnDataSo BarrelSpawnDataSo { get; private set; }
    }
}