using Game.Clothes;
using UnityEngine;

namespace Game.AddOns
{
    public class AddOn_Clothes: AddOn_Base
    {
        [field: SerializeField]
        public Clothes_Base Clothes { get; private set; }

        protected override Transform GfxTransform()
        {
            return Clothes.transform;
        }
    }
}