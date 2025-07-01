using Services;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    private void Awake()
    {
        var serviceLocator = new ServiceLocator();
        var installer = new DependencyInstaller();
        installer.InstallBindings(serviceLocator);
        DontDestroyOnLoad(gameObject);
    }
}