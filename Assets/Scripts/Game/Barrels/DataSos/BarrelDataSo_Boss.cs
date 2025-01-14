using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Barrels.DataSos
{
    [CreateAssetMenu(fileName = "BarrelDataSo_Boss 0", menuName = "BarrelRush/BarrelSpawner/BarrelDataSo_Boss", order = 0)]
    [InlineEditor]
    public class BarrelDataSo_Boss: BarrelDataSo
    {
        [field: SerializeField]
        public int BossType { get; private set; }
    }
}