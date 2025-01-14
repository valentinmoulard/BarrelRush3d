using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.Ui
{
    public class ScaleToMatchInferiorRatio : MonoBehaviour
    {
        [SerializeField]
        private int referenceWidth = 9;

        [SerializeField]
        private int referenceHeight = 16;

        private void Start()
        {
            UpdateScale();
        }

        [Button]
        private void UpdateScale()
        {
            var widthScale = (float)Screen.width / Screen.height * referenceHeight / referenceWidth;
            if(widthScale >= 1.0f)
                widthScale = 1.0f;
            transform.localScale = new Vector3(widthScale, widthScale, 1.0f);
        }
    }
}