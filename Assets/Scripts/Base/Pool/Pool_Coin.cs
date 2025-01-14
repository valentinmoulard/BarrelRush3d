using System;
using Base.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Pool
{
    [Serializable]
    public class Pool_Coin: PoolBase<Image>
    {
        [SerializeField]
        private Transform coinParent;

        protected override void OnGet(Image item)
        {
            base.OnGet(item);
            item.transform.SetParent(coinParent);
            item.transform.ResetTransformLocals();
        }
    }
}