using UnityEngine;
using Base.Ui.Buttons;
using Base.GameManagement;
using Base.Ui;

namespace Game.Ui.Buttons
{
    public class Button_GoToScreen : Button_Base
    {
        [SerializeField]
        private UIScreenType m_screenToGoTo;

        protected override void OnClick()
        {
            base.OnClick();
            ManagersAccess.UIManager.ChangeScreen(m_screenToGoTo);
        }
    }
}
