using Base.GameManagement;
using Game._Soldier;
using Game.Barrels;
using Game.Bullets;

namespace Game.Pools
{
    public static class PoolUtility
    {
        public static void ReturnToPool<T>(this T poolableObject) where T : IPoolable
        {
            var poolManager = ManagersAccess.PoolManager.PoolGameSpecific;

            switch (poolableObject)
            {
                case Bullet_Base @base:
                    poolManager.PoolBullets.ReturnObject(@base);
                    break;
                case Barrel_Base @base:
                    poolManager.PoolBarrel.ReturnObject(@base);
                    break;
                case Module_Manager_Soldier @base:
                    poolManager.PoolSoldier.ReturnObject(@base);
                    break;
            }
        }
    }

    public interface IPoolable
    {

    }
}