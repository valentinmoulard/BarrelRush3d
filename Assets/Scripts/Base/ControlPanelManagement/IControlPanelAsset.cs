namespace Base.ControlPanelManagement
{
    public interface IControlPanelAsset
    {
        int ControlPanelPriority { get; }
        string TreeParentPath { get; }
    }
}