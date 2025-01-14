namespace Game.Modules
{
    public interface IModule
    {
        public void Initialize(Module_Manager moduleManager);
        public ModuleType GetModuleType();
    }
}