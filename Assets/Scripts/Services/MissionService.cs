using System;
using System.Collections.Generic;
using Missions;

namespace Services
{
    public class MissionService : IDisposable
    {
        private readonly HashSet<MissionChain> _activeChains = new HashSet<MissionChain>();
        public event Action<MissionChain> ChainStarted;

        public bool StartChain(MissionChain chain)
        {
            if (_activeChains.Contains(chain))
            {
                return false;
            }

            _activeChains.Add(chain);
            ChainStarted?.Invoke(chain);
            return true;
        }

        public void Dispose()
        {
            _activeChains.Clear();
        }
    }
}