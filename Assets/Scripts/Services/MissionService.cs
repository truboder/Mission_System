using System;
using System.Collections.Generic;
using UnityEngine;
using Missions;

namespace Services
{
    public class MissionService
    {
        private readonly HashSet<MissionChain> _activeChains = new HashSet<MissionChain>();
        public event Action<MissionChain, Action> ChainStarted;

        public bool StartChain(MissionChain chain)
        {
            if (chain == null || chain.Missions == null || chain.Missions.Length == 0)
            {
                return false;
            }

            if (_activeChains.Contains(chain))
            {
                return false;
            }

            _activeChains.Add(chain);
            ChainStarted?.Invoke(chain, () => _activeChains.Remove(chain));
            return true;
        }
    }
}