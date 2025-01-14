using Base.GameManagement;
using Base.Helpers;
using Game.Weapons;
using UnityEngine;

namespace Game.Modules
{
    public abstract class Module_Attack: ModuleBase
    {
        [SerializeField]
        private Transform weaponHolder;
        
        [SerializeField]
        protected Weapon_Base weaponBase;
        
        public override ModuleType GetModuleType()
        {
            return ModuleType.Attack;
        }

        protected void SetWeapon(int weaponIndex)
        {
            var savedWeapon = ManagersAccess.PoolManager.PoolGameSpecific.PoolWeapon.GetObject(weaponIndex);
            ChangeWeapon(savedWeapon);
        }
        
        protected void SetAttackVariables(float attackSpeed, float attackDamage)
        {
            weaponBase.SetAttackVariables(attackSpeed, attackDamage);
        }
        
        public void ChangeWeapon(Weapon_Base weapon)
        {
            Destroy(weaponBase.gameObject);
            weaponBase = weapon;
            weaponBase.transform.SetParent(weaponHolder);
            weaponBase.transform.ResetTransformLocalsButScale();
        }

        public virtual void Attack()
        {
            weaponBase.Attack();
        }
        
        public void StopAttack()
        {
            weaponBase.StopAttack();
        }
    }
}