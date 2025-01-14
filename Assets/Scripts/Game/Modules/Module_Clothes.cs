using Base.Helpers;
using Game._Inventory;
using Game.Clothes;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Modules
{
    public abstract class Module_Clothes: ModuleBase
    {
        [SerializeField, ReadOnly, GUIColor(1, 1f, 1, 1f)]
        private UnitySerializedDictionary<ClothesDataSo, Clothes_Base> clothesHolder;

        [SerializeField, ReadOnly, GUIColor(0.2f, 1f, 0.2f, 1f)]
        private UnitySerializedDictionary<InventoryType, Clothes_Base> activeClothes;

        public override ModuleType GetModuleType()
        {
            return ModuleType.Clothes;
        }
        
        public void AddClothes(ClothesDataSo clothesDataSo)
        {
            var clothesType = clothesDataSo.InventoryType;
            if(activeClothes.ContainsKey(clothesType))
            {
                var clothesBase = activeClothes[clothesType];
                clothesBase.ChangeActiveState(false);
                activeClothes.Remove(clothesType);

            }
            var clothesBaseNew = clothesHolder[clothesDataSo];
            clothesBaseNew.ChangeActiveState(true);
            activeClothes.Add(clothesType, clothesBaseNew);
        }

#if UNITY_EDITOR
        [ContextMenu("Create Clothes Holder")]
        private void CreateClothesHolder()
        {
            clothesHolder = new UnitySerializedDictionary<ClothesDataSo, Clothes_Base>();
            var clothesBases = GetComponentsInChildren<Clothes_Base>(true);
            foreach (var clothesBase in clothesBases)
            {
                clothesHolder.Add(clothesBase.ClothesDataSo, clothesBase);
            }
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(gameObject);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}