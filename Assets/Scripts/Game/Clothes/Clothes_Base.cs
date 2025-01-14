using Game._Equipments;
using UnityEngine;

namespace Game.Clothes
{
    public class Clothes_Base: Equipment_Base
    {
        [field: SerializeField]
        public ClothesDataSo ClothesDataSo { get; private set; }

        public void ChangeActiveState(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}