using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Barrels
{
    [CreateAssetMenu(fileName = "BarrelSpawnerData", menuName = "BarrelRush/BarrelSpawner/BarrelSpawnerData", order = 0)]
    [InlineEditor]
    public class BarrelSpawnDataSo : ScriptableObject
    {
        [ShowInInspector]
        [GUIColor( 0.2f, 1, 0.5f, 1f)]
        public float TotalBarrelCount => HowManyBarrels();

        private float HowManyBarrels()
        {
            var count = 0;
            foreach (var barrelAndCountPair in DefaultBarrelSquat)
            {
                count += barrelAndCountPair.HowManyObject;
            }

            return count;
        }
        
        [field: SerializeField, ListDrawerSettings(ShowIndexLabels = true)]
        public ObjectAndCountPair_Default[] DefaultBarrelSquat { get; private set; }
    }
}