using UnityEngine;
using Base.GameManagement;
using Base.SaveSystem.SaveableScriptableObject;
using Game.FightSystem;
using Game.Weapons;
using Game.Ui.Buttons;
using TMPro;
using Game.SaveableSos;
using Game.Events;
using Game.SaveData;

namespace Game.Ui
{
    public abstract class UI_EquipmentInfoCardFrame<T> : UI_InfoCardFrame<T> where T : SaveableSo_Equipment
    {
        [Header("UI elements")]
        [SerializeField]
        private GameObject equippedStateObject = null;

        [Header("Buttons")]
        [SerializeField]
        private Button_UnlockItem unlockButton = null;

        [SerializeField]
        private TMP_Text unlockButtonText = null;

        [SerializeField]
        private Button_EquipItem equipButton = null;

        private EquipmentState EquipmentState => dataSo.GetData();

        protected override void OnEnable()
        {
            if (dataSo == null)
                return;

            unlockButton.OnClickUnlockItemButton += TryUnlockEquipment;
            equipButton.OnClickEquipButton += TryEquipEquipment;
            Events_AddOn.OnUpdateEquipmentState += UpdateInventoryEquippedState;

            base.OnEnable();
        }

        protected override void OnDisable()
        {
            if (dataSo == null)
                return;

            unlockButton.OnClickUnlockItemButton -= TryUnlockEquipment;
            equipButton.OnClickEquipButton -= TryEquipEquipment;
            Events_AddOn.OnUpdateEquipmentState -= UpdateInventoryEquippedState;

            base.OnDisable();
        }

        private void TryUnlockEquipment()
        {
            if (dataSo == null)
            {
                Debug.LogError("No data referenced!", gameObject);
                return;
            }

            float unlockCost = dataSo.UnlockData.UnlockCost;
            bool canUnlock = ManagersAccess.CoinManager.HasEnoughtCoin(unlockCost);


            if (canUnlock)
            {
                dataSo.IsUnlocked = true;
                ManagersAccess.CoinManager.ReduceCoin(unlockCost);
            }
        }

        private void UpdateInventoryEquippedState()
        {
            var inventoryData = ManagersAccess.SaveManager.GetSaveData<SaveData_Inventory>(SaveDataType.InventoryData);
            var inventoryDictionary = inventoryData.Dictionary;

            foreach (var inventoryItem in inventoryDictionary)
            {
                if (inventoryItem.Value.UniqueID == dataSo.UniqueID)
                {
                    equippedStateObject.SetActive(true);
                    equipButton.gameObject.SetActive(false);
                    
                    if(!dataSo.IsEquipped)
                    {
                        dataSo.IsEquipped = true;
                        Events_AddOn.OnInventoryUpdated?.Invoke();
                    }
                    
                    return;
                }
            }

            equippedStateObject.SetActive(false);
            equipButton.gameObject.SetActive(dataSo.IsUnlocked);
            
            if (dataSo.IsEquipped)
                dataSo.IsEquipped = false;
        }

        private void TryEquipEquipment()
        {
            if (dataSo == null)
            {
                Debug.LogError("No data referenced!", gameObject);
                return;
            }

            Events_AddOn.OnEquipmentEquipped?.Invoke(dataSo);
            ManagersAccess.SaveManager.GetSaveData<SaveData_Inventory>(SaveDataType.InventoryData).SetData(dataSo.InventoryType, dataSo);
            Events_AddOn.OnUpdateEquipmentState?.Invoke();
        }

        protected override void UpdateInfoCard()
        {
            base.UpdateInfoCard();

            EquipmentState equipmentState = EquipmentState;

            equippedStateObject.SetActive(equipmentState.IsEquipped);
            
            UpdateStatsText(equipmentState.Level);

            UpdateLockedState(equipmentState);
        }

        private void UpdateLockedState(EquipmentState equipmentState)
        {
            if (dataSo.IsOnMaxLevel)
            {
                upgradeButtonText.text = "MAX";
                upgradeButton.ChangeActiveState(false);
            }
            else
            {
                if (equipmentState.IsUnlocked == false)
                {
                    unlockButton.gameObject.SetActive(true);

                    stringBuilder.Clear();
                    stringBuilder.Append(dataSo.UnlockData.UnlockCost.ToString("F0"));
                    stringBuilder.Append(ManagersAccess.CoinManager.CoinAssetPath);
                    unlockButtonText.text = stringBuilder.ToString();

                    float unlockCost = dataSo.UnlockData.UnlockCost;
                    bool canUnlock = ManagersAccess.CoinManager.HasEnoughtCoin(unlockCost);

                    unlockButton.Button.interactable = canUnlock;

                    upgradeButton.gameObject.SetActive(false);
                }
                else
                {
                    upgradeButton.gameObject.SetActive(true);

                    float upgradeCost = dataSo.GetCost(EquipmentState.Level);
                    bool canPurchaseUpgrade = ManagersAccess.CoinManager.HasEnoughtCoin(upgradeCost);

                    upgradeButton.Button.interactable = canPurchaseUpgrade;

                    unlockButton.gameObject.SetActive(false);
                }
            }

            UpdateInventoryEquippedState();
        }
    }
}