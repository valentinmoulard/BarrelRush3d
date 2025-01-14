using UnityEngine;

namespace Base.Ui
{
    public sealed class SafeAreaRectTransform : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;

        static Rect SafeArea => Screen.safeArea;

        private void Start()
        {
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            var safeArea = SafeArea;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

        private void OnValidate()
        {
            if(!rectTransform)
                rectTransform = GetComponent<RectTransform>();
        }
    }
}