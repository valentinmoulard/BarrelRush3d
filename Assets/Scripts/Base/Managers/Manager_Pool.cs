using Base.Particles;
using Base.Pool;
using Game.Pools;
using UnityEngine;

namespace Base.Managers
{
    public class Manager_Pool: ManagerBase
    {
        [field: SerializeField]
        public Pool_Coin PoolCoin { get; private set; }

        [field: SerializeField]
        public Pool_Particle PoolParticle { get; private set; }

        [field: SerializeField]
        public Pool_GameSpecific PoolGameSpecific { get; private set; }

        public override void SetUp()
        {
            base.SetUp();
            PoolGameSpecific.InitializePools();
            PoolCoin.InitializePools();
            PoolParticle.InitializePools();
        }
    }
}