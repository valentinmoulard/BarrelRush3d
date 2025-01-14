using System.Collections.Generic;
using Base.GameManagement;
using DG.Tweening;
using Game._Inventory;
using Game.Clothes;
using Game.Events;
using Game.FightSystem;
using Game.Modules;
using Game.SaveableSos;
using Game.Weapons;
using UnityEngine;

namespace Game._Hero
{
    public class Module_Attack_Hero: Module_Attack
    {
        private Tween _updateInventoryTween;
        private readonly Dictionary<InventoryType, SaveableSo_Equipment> _inventorySoDictionary = new Dictionary<InventoryType, SaveableSo_Equipment>();
        private bool _isSetUp;
        
        [SerializeField]
        private ParticleSystem upgradeParticle;
        
        public void SetInventorySo(SaveableSo_Equipment inventorySo)
        {
            _inventorySoDictionary[inventorySo.InventoryType] = inventorySo;
            UpdateAttackVariables();
        }

        private void UpdateAttackVariables()
        {
            float attackDamage = 0;
            float attackSpeed = 0;
            float profitMultiplier = 0;
            
            WeaponDataSo weaponDataSo = _inventorySoDictionary[InventoryType.Weapon] as WeaponDataSo;
            var fightData = weaponDataSo.FightData;
            var upgradeLevel = weaponDataSo.UpgradeIndex;
            attackDamage += fightData.GetFightDataValue(FightDataValueType.AttackDamage, upgradeLevel);
            attackSpeed += fightData.GetFightDataValue(FightDataValueType.AttackSpeed, upgradeLevel);
            SetWeapon(weaponDataSo.EquipPrefabIndex);
            
            foreach (var inventorySo in _inventorySoDictionary)
            {
                var inventoryType = inventorySo.Key;
                var inventoryData = inventorySo.Value;
                var upgradeIndex = inventoryData.UpgradeIndex;
                if (inventoryType != InventoryType.Weapon)
                {
                    var clothesData = ((ClothesDataSo)inventoryData).ClothesData;
                    attackDamage += clothesData.GetClotheDataValue(ClotheDataValueType.BonusAttackDamage, upgradeIndex);
                    attackSpeed += clothesData.GetClotheDataValue(ClotheDataValueType.BonusAttackSpeed, upgradeIndex);
                    profitMultiplier += clothesData.GetClotheDataValue(ClotheDataValueType.ProfitMultiplier, upgradeIndex);
                }
            }
            
            Events_Inventory.OnInventoryUpdated?.Invoke(attackSpeed, attackDamage, profitMultiplier);
            SetAttackVariables(attackSpeed, attackDamage);
            ManagersAccess.CoinManager.SetProfitMultiplier(profitMultiplier);
            SetUpgradeEffects();
        }

        private void SetUpgradeEffects()
        {
            if(!_isSetUp)
            {
                _isSetUp = true;
                return;
            }
            upgradeParticle.Play();
            if(_updateInventoryTween != null)
                _updateInventoryTween.Kill();
            _updateInventoryTween = UpdateInventoryTween();
        }

        private Tween UpdateInventoryTween()
        {
            return transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
        }
    }
}