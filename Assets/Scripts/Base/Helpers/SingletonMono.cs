using UnityEngine;

namespace Base.Helpers
{
    [DefaultExecutionOrder(int.MinValue + 10)]
    public abstract class SingletonMono<T> : MonoBehaviour where T: MonoBehaviour
    {
        public bool dontDestroyOnLoad;
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if (!Application.isPlaying)
                {
                    s_instance = FindObjectOfType<T>();
                }
                BuildNewInstanceIfNull();

                return s_instance;
            }
            set => s_instance = value;
        }

        private static void BuildNewInstanceIfNull()
        {
            if (s_instance == null)
            {
                var newObject = new GameObject("" + typeof(T).Name);
                s_instance = newObject.AddComponent<T>();
            }
        }

        protected virtual void Awake()
        {
            if(s_instance == null)
            {
                s_instance = this as T;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            (Instance as SingletonMono<T>)?.UseThisInsteadOfAwake();
            SetIfDontDestroyOnLoad();
        }

        private void SetIfDontDestroyOnLoad()
        {
            if (!dontDestroyOnLoad) return;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }

        protected abstract void UseThisInsteadOfAwake();

        protected virtual void ForceToDontDestroyedOnLoad()
        {
            dontDestroyOnLoad = true;
        }
    }
}