using Base.GameManagement;
using UnityEngine;

namespace Base.Managers
{
    public class Manager_Joystick : ManagerBase
    {
        [field: SerializeField]
        public bool IsWaiting { get; set; }

        [field: SerializeField]
        public float HorizontalInput { get; protected set; }
        
        [field: SerializeField]
        public bool InputHold { get; protected set; }

        private bool IsPlaying => ManagersAccess.GameStateController.IsPlaying;
        private void Update()
        {
            if (IsWaiting || !IsPlaying)
            {
                StopInput();
                return;
            }
            HandleInput();
        }

        private void StopInput()
        {
            if(HorizontalInput > 0)
                HorizontalInput = 0;
        }

        public void ChangeWaiting(bool isWaiting)
        {
            IsWaiting = isWaiting;
        }

        protected virtual void HandleInput() 
        {
            InputHold = Input.GetMouseButton(0);
            if (!InputHold) return;
            if (IsWaiting) return;
            HorizontalInput = Input.GetAxis("Mouse X");
        }
    }
}
