using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator serviceLocator = new ServiceLocator();
            DependencyInstaller installer = new DependencyInstaller();
            installer.InstallBindings(serviceLocator);

            List<IInitializable> initializables = serviceLocator.GetService<List<IInitializable>>();
            
            foreach (IInitializable initializable in initializables)
            {
                initializable.Initialize();
            }

            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            List<IDisposable> disposables = ServiceLocator.Instance.GetService<List<IDisposable>>();
            
            foreach (IDisposable disposable in disposables)
            {
                disposable.Dispose();
            }
        }
    }
}