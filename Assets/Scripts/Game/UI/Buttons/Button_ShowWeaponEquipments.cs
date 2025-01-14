using Base.Ui.Buttons;

namespace Game.Ui.Buttons
{
    public class Button_ShowWeaponEquipments : Button_Base
    {
        public System.Action OnClickShowWeaponEquipments;

        protected override void OnClick()
        {
            OnClickShowWeaponEquipments?.Invoke();
        }
    }
}