using Game._Inventory;
using Game.EconomySystem;
using Game.Events;
using Game.SaveData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.SaveableSos
{
    public class SaveableSo_Equipment: SaveableSo
    {
        [field: SerializeField, BoxGroup("Inventory"), GUIColor(0.8f, 0.2f, 0.8f)]
        public InventoryType InventoryType { get; private set; }

        [field: SerializeField]
        public UnlockData UnlockData { get; private set; }
        
        protected virtual SaveData_Equipment GetEquipmentSaveData()
        {
            throw new System.NotImplementedException();
        }
        
        public EquipmentState GetData()
        {
            return GetEquipmentSaveData().GetData(UniqueID);
        }

        public bool IsUnlocked {
            get => GetData().IsUnlocked;
            set
            {
                var data = GetData();
                data.IsUnlocked = value;
                GetEquipmentSaveData().SetData(UniqueID, data);
                Events_AddOn.OnInventoryUpdated?.Invoke();
            }
        }

        public bool IsEquipped {
            get => GetData().IsEquipped;
            set
            {
                var data = GetData();
                data.IsEquipped = value;
                GetEquipmentSaveData().SetData(UniqueID, data);
            }
        }

        public override int UpgradeIndex {
            get => GetData().Level;
            set
            {
                var data = GetData();
                data.Level = value;
                GetEquipmentSaveData().SetData(UniqueID, data);
                Events_AddOn.OnInventoryUpdated?.Invoke();
            }
        }
    }
}