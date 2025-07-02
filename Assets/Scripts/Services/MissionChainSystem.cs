using System;
using System.Collections.Generic;
using Infrastructure;
using Missions;

namespace Services
{
    public class MissionChainSystem : IInitializable, IDisposable
    {
        private const int MillisecondsPerSecond = 1000;
        
        private readonly Dictionary<MissionChain, ActiveChain> _activeChains = new Dictionary<MissionChain, ActiveChain>();
        private readonly MissionService _missionService;

        public MissionChainSystem(MissionService missionService)
        {
            _missionService = missionService;
        }

        public void Initialize()
        {
            _missionService.ChainStarted += OnChainStarted;
        }

        public void Dispose()
        {
            _missionService.ChainStarted -= OnChainStarted;
        }

        private void OnChainStarted(MissionChain chain)
        {
            if (_activeChains.ContainsKey(chain))
            {
                return;
            }

            ActiveChain activeChain = new ActiveChain 
            { 
                Chain = chain, 
                Index = -1, 
                OnCompleted = () => _missionService.StartChain(null)
            };
            
            _activeChains[chain] = activeChain;
            StartNextMission(chain);
        }

        private async void StartNextMission(MissionChain chain)
        {
            if (!_activeChains.TryGetValue(chain, out ActiveChain activeChain))
            {
                return;
            }

            activeChain.Index++;
            _activeChains[chain] = activeChain;

            MissionBase mission = chain.Missions[activeChain.Index];

            if (mission.StartDelaySeconds > 0)
            {
                Timer timer = new Timer();
                await timer.StartAsync((int)(mission.StartDelaySeconds * MillisecondsPerSecond));
            }

            mission.OnFinished += () => OnMissionCompleted(chain);
            mission.Start();
        }

        private void OnMissionCompleted(MissionChain chain)
        {
            if (!_activeChains.TryGetValue(chain, out ActiveChain activeChain))
            {
                return;
            }
            
            if (activeChain.Index >= chain.Missions.Length - 1)
            {
                _activeChains.Remove(chain);
                activeChain.OnCompleted?.Invoke();
                return;
            }

            StartNextMission(chain);
        }
    }
}