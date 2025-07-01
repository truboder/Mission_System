using System.Collections.Generic;
using Missions;
using UnityEngine;

namespace Services
{
    public class MissionService : MonoBehaviour
    {
        private MissionSystem missionSystem;
        private HashSet<MissionChain> activeChains = new HashSet<MissionChain>();

        private void Awake()
        {
            missionSystem = gameObject.AddComponent<MissionSystem>();
        }

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
            missionSystem.StartChain(chain, () => activeChains.Remove(chain));
            return true;
        }
    }
}