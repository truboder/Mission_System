using Services;

public class DependencyInstaller
{
    public void InstallBindings(ServiceLocator locator)
    {
        locator.RegisterService(new MissionService());
        locator.RegisterService(new MissionSystem());
    }
}