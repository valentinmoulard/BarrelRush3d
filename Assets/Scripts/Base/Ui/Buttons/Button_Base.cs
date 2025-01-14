using Base.Helpers;
using UnityEngine;
using UnityEngine.UI;
using Color = System.Drawing.Color;

namespace Base.Ui.Buttons
{
    public abstract class Button_Base: EventListener
    {
        [field: SerializeField]
        public Button Button { get; private set; }

        [SerializeField]
        private bool isRv;

        protected override void SubscribeEvents()
        {
            if(Button)
                Button.onClick.AddListener(OnClick);
        }

        protected override void UnsubscribeEvents()
        {
            if(Button)
                Button.onClick.RemoveListener(OnClick);
        }
        
        public void ChangeActiveState(bool isActive)
        {
            if (Button)
            {
                Button.interactable = isActive;
            }
        }

        protected virtual void OnClick()
        {
            // YufisDebug.Log($"{name} clicked", Color.DeepSkyBlue);
        }

        #region UNITY_EDITOR

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Button == null)
            {
                Button = GetComponent<Button>();
                if (Button == null)
                {
                    YufisDebug.Log("There is no button component on this object" + gameObject.name, Color.DarkRed);
                }
            }
        }
#endif

        #endregion

    }
}