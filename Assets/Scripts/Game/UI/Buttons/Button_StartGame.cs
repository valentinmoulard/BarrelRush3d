using Base.GameManagement;
using UnityEngine.EventSystems;
using Base.Ui;
using Base.Ui.Buttons;

namespace Game.Ui.Buttons
{
    public class Button_StartGame : Button_Base, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            if(Button.interactable)
            {
                ManagersAccess.GameStateController.SetState(UIScreenType.Playing);
                enabled = false;
            }
        }
    }
}