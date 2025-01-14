using System;
using Base.Events;
using Base.GameManagement;
using Base.SaveSystem.SaveableScriptableObject;
using Base.Ui;
using Game._Inventory;
using Game._Soldier;
using Game.AddOns;
using Game.Clothes;
using Game.Events;
using Game.Modules;
using Game.SaveableSos;
using Game.SaveData;
using Game.Weapons;

namespace Game._Hero
{
    public class Module_Manager_Hero: Module_Manager
    {
        private Module_UnitHolder_Hero _unitHolderModule;
        private Module_Attack_Hero _attackModule;
        private Module_Clothes_Hero _clothesModule;
        private Module_Movement_Hero _movementModule;
        private Module_Health_Hero _healthModule;
        public HeroState HeroState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _unitHolderModule = GetModule<Module_UnitHolder_Hero>(ModuleType.UnitHolder);
            _attackModule = GetModule<Module_Attack_Hero>(ModuleType.Attack);
            _clothesModule = GetModule<Module_Clothes_Hero>(ModuleType.Clothes);
            _movementModule = GetModule<Module_Movement_Hero>(ModuleType.Movement);
            _healthModule = GetModule<Module_Health_Hero>(ModuleType.Health);
            _healthModule.ChangeCanDie(true);
            SubscribeToEvents();
        }

        protected override void Start()
        {
            base.Start();
            UpdateInventory();
        }

        private void UpdateInventory()
        {
            var inventoryData = ManagersAccess.SaveManager.GetSaveData<SaveData_Inventory>(SaveDataType.InventoryData);
            var inventoryDictionary = inventoryData.Dictionary;
            foreach (var inventoryItem in inventoryDictionary)
            {
                _attackModule.SetInventorySo(inventoryItem.Value);
                if(inventoryItem.Key != InventoryType.Weapon)
                    _clothesModule.AddClothes((ClothesDataSo) inventoryItem.Value);
            }
        }
        
        private void SubscribeToEvents()
        {
            EventsGame.OnGameStateChanged += OnGameStateChanged;
            Events_AddOn.OnAddActivated += OnAddOnActivated;
            Events_AddOn.OnEquipmentEquipped += OnEquipmentEquipped;
            Events_AddOn.OnInventoryUpdated += UpdateInventory;
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsGame.OnGameStateChanged -= OnGameStateChanged;
            Events_AddOn.OnAddActivated -= OnAddOnActivated;
            Events_AddOn.OnEquipmentEquipped -= OnEquipmentEquipped;
            Events_AddOn.OnInventoryUpdated -= UpdateInventory;
        }

        private void OnEquipmentEquipped(SaveableSo_Equipment equipmentDataSo)
        {
            var dataSoType = equipmentDataSo.GetType();
            switch (dataSoType)
            {
                case not null when dataSoType == typeof(WeaponDataSo):
                    var dataSo = (WeaponDataSo) equipmentDataSo;
                    var weaponBase = ManagersAccess.PoolManager.PoolGameSpecific.PoolWeapon.GetObject(dataSo.EquipPrefabIndex);
                    _attackModule.ChangeWeapon(weaponBase);
                    break;
                case not null when dataSoType == typeof(ClothesDataSo):
                    _clothesModule.AddClothes((ClothesDataSo) equipmentDataSo);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(equipmentDataSo), equipmentDataSo, null);
            }
        }

        private void OnAddOnActivated(AddOn_Base addOnObject)
        {
            addOnObject.ChangeSize(false);
            switch (addOnObject)
            {
                case AddOn_Soldier addOnSoldier:
                    AddSoldier(addOnSoldier.Soldier);
                    break;
                case AddOn_Weapons addOnWeapons:
                    _attackModule.ChangeWeapon(addOnWeapons.Weapon);
                    break;
                // case AddOn_Clothes addOnClothes: //todo fix if we can collect clothes as an object
                //     _clothesModule.AddClothes(addOnClothes.Clothes);
                //     break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(addOnObject), addOnObject, null);
            }
        }

        private void OnGameStateChanged(UIScreenType state, bool isEnabled)
        {
            switch (state)
            {
                case UIScreenType.Generic:
                    break;
                case UIScreenType.Start:
                    if (isEnabled)
                        ChangeState(HeroState.Idle);
                    break;
                case UIScreenType.Playing:
                    if (isEnabled)
                    {
                        ChangeState(HeroState.Attack);
                    }
                    break;
                case UIScreenType.Win:
                    if (isEnabled)
                        ChangeState(HeroState.Win);
                    break;
                case UIScreenType.Lose:
                    break;
                case UIScreenType.SoldierUpgrade:
                    break;
                case UIScreenType.WeaponUpgrade:
                    break;
                case UIScreenType.WaitingForRevive:
                    if (isEnabled)
                        ChangeState(HeroState.WaitForRevive);
                    else
                    {
                        SendHeroBack();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void SendHeroBack()
        {
            var position = transform.position;
            position.y = 0;
            position.z -= 1;
            transform.position = position;
        }

        public void ChangeState(HeroState state)
        {
            HeroState = state;
            ChangeInputWaiting(true);
            switch (HeroState)
            {
                case HeroState.Idle:
                    Idle();
                    break;
                case HeroState.Attack:
                    ChangeInputWaiting(false);
                    Attack();
                    break;
                case HeroState.Lose:
                    Die();
                    break;
                case HeroState.Win:
                    Celebrate();
                    break;
                case HeroState.WaitForRevive:
                    WaitForRevive();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _unitHolderModule.RestartTheSquatBehaviourRoutine(true);
        }

        private void ChangeInputWaiting(bool isWaiting)
        {
            _movementModule.ChangeOnWaiting(isWaiting);
        }

        private void AddSoldier(Module_Manager_Soldier soldier)
        {
            _unitHolderModule.AddSoldier(soldier);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnsubscribeFromEvents();
        }
    }
}