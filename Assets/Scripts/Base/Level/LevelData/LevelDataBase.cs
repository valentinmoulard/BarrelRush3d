using System.Collections.Generic;
using Game._LevelData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.Level.LevelData
{
    [InlineEditor()]
    [CreateAssetMenu(fileName = "Level 1", menuName = "YufisBase/LevelData/LevelDataAsset", order = 0)]
    public class LevelDataBase : ScriptableObject
    {
        [field: SerializeField, GUIColor( 0.5f, 0.8f, 1f, 1f)]
        public LevelController LevelPrefab { get; private set; }

        [field: SerializeField, ListDrawerSettings(ShowIndexLabels = true)]
        public List<BarrelLevelData> BarrelLevelData { get; private set; }
    }
}