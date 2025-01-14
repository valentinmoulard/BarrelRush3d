using System.Collections;
using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Helpers;
using Game._Equipments;
using UnityEngine;

namespace Game.Weapons
{
    public class Weapon_Base: Equipment_Base
    {
        [SerializeField]
        private int bulletIndex;
        
        [SerializeField]
        private ParticleSystem attackEffect;

        [SerializeField]
        private Transform bulletSpawnPoint;

        private Coroutine _attackCoroutine;
        private float _attackSpeed;
        private float _attackDamage;
        
        public void SetAttackVariables(float attackSpeed, float attackDamage)
        {
            _attackSpeed = attackSpeed;
            _attackDamage = attackDamage;
        }

        public void Attack()
        {
            ResetCoroutine();
            if(gameObject.activeInHierarchy)
                _attackCoroutine = StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            while (true)
            {
                SendBullet();
                yield return CoroutineExtensions.WaitForSeconds(1 / _attackSpeed);
            }
        }

        protected virtual void SendBullet()
        {
            attackEffect.Play();
           var bullet = ManagersAccess.PoolManager.PoolGameSpecific.PoolBullets.GetObject(0);
           bullet.SetUp(bulletSpawnPoint.position, _attackDamage, bulletIndex);
           var settingsBullet = Settings_General.Instance.GameSettings.SettingsBullet;
           bullet.MoveToTarget(Vector3.forward * settingsBullet.Distance, settingsBullet.Duration);
        }

        public void StopAttack()
        {
            ResetCoroutine();
        }
        
        private void ResetCoroutine()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }
    }
}