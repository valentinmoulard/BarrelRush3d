namespace Game.Modules
{
    public abstract class Module_Collision: ModuleBase
    {
        public override ModuleType GetModuleType()
        {
            return ModuleType.Collision;
        }
    }
}