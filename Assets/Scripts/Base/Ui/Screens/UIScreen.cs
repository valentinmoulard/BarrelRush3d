using UnityEngine;

namespace Base.Ui.Screens
{
    public class UIScreen: MonoBehaviour
    {
        [field: SerializeField]
        public UIScreenType UiScreenType { get; private set; }
        
        [SerializeField]
        private CanvasGroup canvasGroup;

        public void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        public void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void Reset()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}