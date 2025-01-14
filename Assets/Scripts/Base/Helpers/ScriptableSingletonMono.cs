using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Base.Helpers
{
    public abstract class ScriptableSingletonMono<T> : ScriptableObject where T : ScriptableSingletonMono<T>
    {
        private static T s_instance;
        public static T Instance {
            get {
                if (s_instance == null)
                {
                    s_instance = Resources.Load<T>(typeof(T).Name);
                }
                return s_instance;
            }
        }

        private void Awake()
        {
            s_instance = Resources.Load<T>(typeof(T).Name);
        }
#if UNITY_EDITOR

        protected virtual void OnValidate()
        {
            DeleteSameTypes();
            MoveToResourcesFolder();
        }

        private void MoveToResourcesFolder()
        {
            if (!AssetDatabase.GetAssetPath(this).Contains("Resources"))
            {
                AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(this), "Assets/Resources/" + this.name + ".asset");
            }
        }

        private void DeleteSameTypes()
        {
            if (Resources.FindObjectsOfTypeAll<T>().Length > 1)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));
            }
        }
#endif

    }
}