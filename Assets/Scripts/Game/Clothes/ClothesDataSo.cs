using Base.GameManagement;
using Base.SaveSystem.SaveableScriptableObject;
using Game.SaveableSos;
using Game.SaveData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Clothes
{
    [CreateAssetMenu(fileName = "ClothesDataSo", menuName = "BarrelRush/Clothes/ClothesDataSo", order = 0)]
    public class ClothesDataSo : SaveableSo_Equipment
    {
        [field: SerializeField, GUIColor(0.2f, 0.8f, 0.8f)]
        public ClothesData ClothesData { get; private set; }
        
        protected override SaveData_Equipment GetEquipmentSaveData()
        {
            return ManagersAccess.SaveManager.GetSaveData<SaveData_Equipment>(SaveDataType.ClothesData);
        }
    }
}