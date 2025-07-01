using Services;

namespace Infrastructure
{
    public class DependencyInstaller
    {
        public void InstallBindings(ServiceLocator locator)
        {
            locator.RegisterService(new MissionService());
        }
    }
}