using UnityEngine;

namespace Base.Level.LevelData
{
    [CreateAssetMenu(fileName = "LevelDataHolder", menuName = "YufisBase/LevelData/LevelDataHolder", order = 0)]
    public class LevelDataHolder : ScriptableObject
    {
        [SerializeField]
        private LevelDataBase[] levelDataAssets;

        public LevelDataBase GetLevelData(int levelIndex)
        {
            return levelDataAssets[levelIndex % levelDataAssets.Length];
        }
    }
}