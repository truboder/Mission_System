using Services;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            var serviceLocator = new ServiceLocator();
            var installer = new DependencyInstaller();
            
            installer.InstallBindings(serviceLocator);
            
            var missionService = serviceLocator.GetService<MissionService>();
            new MissionChainExecutor(missionService);
            DontDestroyOnLoad(gameObject);
        }
    }
}