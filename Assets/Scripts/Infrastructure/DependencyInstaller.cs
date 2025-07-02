using System;
using System.Collections.Generic;
using Services;

namespace Infrastructure
{
    public class DependencyInstaller
    {
        private readonly List<IInitializable> _initializables = new List<IInitializable>();
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public void InstallBindings(ServiceLocator locator)
        {
            var missionService = new MissionService();
            locator.RegisterService(missionService);
            _initializables.Add(new MissionChainSystem(missionService));
            _disposables.Add(missionService);

            locator.RegisterService(_initializables);
            locator.RegisterService(_disposables);
        }
    }
}