using System.Collections.Generic;
using Missions;
using UnityEngine;

namespace Services
{
    public class MissionService
    {
        private readonly HashSet<MissionChain> activeChains = new HashSet<MissionChain>();

        public bool StartChain(MissionChain chain)
        {
            if (chain == null || chain.Missions == null || chain.Missions.Length == 0)
            {
                Debug.LogWarning("Invalid mission chain.");
                return false;
            }

            if (activeChains.Contains(chain))
            {
                Debug.LogWarning("Mission chain is already running.");
                return false;
            }

            activeChains.Add(chain);
            MissionSystem missionSystem = ServiceLocator.Instance.GetService<MissionSystem>();
            missionSystem.StartChain(chain, () => activeChains.Remove(chain));
            return true;
        }
    }
}