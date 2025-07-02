using System;
using System.Collections.Generic;
using Missions;

namespace Services
{
    public class MissionService
    {
        private readonly HashSet<MissionChain> _activeChains = new HashSet<MissionChain>();
        public event Action<MissionChain> ChainStarted;

        public bool StartChain(MissionChain chain)
        {
            _activeChains.Add(chain);
            ChainStarted?.Invoke(chain);
            return true;
        }
    }
}