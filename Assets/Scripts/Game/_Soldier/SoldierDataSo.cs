using Base.GameManagement;
using Base.SaveSystem;
using Base.SaveSystem.SaveableScriptableObject;
using Game.EconomySystem;
using Game.FightSystem;
using Game.SaveableSos;
using UnityEngine;

namespace Game._Soldier
{
    [CreateAssetMenu(fileName = "SoldierDataSo", menuName = "BarrelRush/Soldiers/SoldierDataSo", order = 0)]
    public sealed class SoldierDataSo : SaveableSo
    {
        [field: SerializeField]
        public FightData FightData { get; private set; }

        [field: SerializeField]
        public UnlockData UnlockData { get; private set; }

        // Gets the data of the corresponding save data type from the save manager (called in parent SaveeableSo)
        protected override SaveData_UintDictionary GetSaveData()
        {
            return ManagersAccess.SaveManager.GetSaveData<SaveData_UintDictionary>(SaveDataType.SoldiersData);
        }
    }
}