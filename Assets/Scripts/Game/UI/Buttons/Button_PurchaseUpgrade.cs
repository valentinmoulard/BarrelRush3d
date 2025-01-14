using Base.Ui.Buttons;

namespace Game.Ui.Buttons
{
    public class Button_PurchaseUpgrade : Button_Base
    {
        public System.Action OnClickPurchaseUpgradeButton;

        protected override void OnClick()
        {
            OnClickPurchaseUpgradeButton?.Invoke();
        }
    }
}