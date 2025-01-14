using UnityEngine;

namespace Game.AddOns
{
    public class AddOn_Base: MonoBehaviour
    {
        protected virtual Transform GfxTransform()
        {
            throw new System.NotImplementedException();
        }
        
        public void ChangeSize(bool isBig)
        {
            GfxTransform().localScale = isBig ? Vector3.one * 2 : Vector3.one;
        }
    }
}