using System.Collections.Generic;
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

            var initializables = serviceLocator.GetService<List<IInitializable>>();
            
            foreach (var initializable in initializables)
            {
                initializable.Initialize();
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}