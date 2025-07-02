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
            MissionService missionService = new MissionService();
            MissionChainSystem missionChainSystem = new MissionChainSystem(missionService);

            locator.RegisterService(missionService);
            locator.RegisterService(missionChainSystem);

            _initializables.Add(missionChainSystem);
            _disposables.Add(missionService);
            _disposables.Add(missionChainSystem);

            locator.RegisterService(_initializables);
            locator.RegisterService(_disposables);
        }
    }
}