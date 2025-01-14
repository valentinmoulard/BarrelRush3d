using UnityEngine;

namespace Game.Pools
{
    public class Pool_GameSpecific: MonoBehaviour
    {
        [field: SerializeField]
        public Pool_Barrel PoolBarrel { get; private set; }

        [field: SerializeField]
        public Pool_Bullets PoolBullets { get; private set; }

        [field: SerializeField]
        public Pool_Soldier PoolSoldier { get; private set; }

        [field: SerializeField]
        public Pool_Weapon PoolWeapon { get; private set; }

        [field: SerializeField]
        public Pool_Bosses PoolBosses { get; private set; }
        
        public void InitializePools()
        {
            PoolBarrel.InitializePools();
            PoolBullets.InitializePools();
            PoolSoldier.InitializePools();
            PoolWeapon.InitializePools();
            PoolBosses.InitializePools();
        }
    }
}