namespace Base.Ui.Buttons
{
    public class Button_NextLevel: Button_Base
    {
        protected override void OnClick()
        {
            base.OnClick();
            LevelUp();
            ReloadScene();
        }
        
        private void LevelUp()
        {
            
        }
        
        private void ReloadScene()
        {
            
        }
    }
}