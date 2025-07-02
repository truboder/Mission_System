using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Missions;

namespace Services
{
    public class MissionChainExecutor
    {
        private readonly Dictionary<MissionChain, ActiveChain> _activeChains = new Dictionary<MissionChain, ActiveChain>();

        public MissionChainExecutor(MissionService missionService)
        {
            missionService.ChainStarted += OnChainStarted;
        }

        private void OnChainStarted(MissionChain chain)
        {
            if (_activeChains.ContainsKey(chain)) return;

            var activeChain = new ActiveChain { Chain = chain, Index = 0 };
            _activeChains[chain] = activeChain;
            StartMission(chain.Missions[0], chain);
        }

        private async void StartMission(MissionBase mission, MissionChain chain)
        {
            if (mission.StartDelaySeconds > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(mission.StartDelaySeconds));
            }

            mission.OnFinished += () => OnMissionCompleted(mission, chain);
            mission.Start();
        }

        private void OnMissionCompleted(MissionBase mission, MissionChain chain)
        {
            if (!_activeChains.TryGetValue(chain, out ActiveChain activeChain)) return;
            
            if (activeChain.Index >= chain.Missions.Length - 1)
            {
                _activeChains.Remove(chain);
                activeChain.OnCompleted?.Invoke();
                return;
            }

            activeChain.Index++;
            _activeChains[chain] = activeChain;
            StartMission(chain.Missions[activeChain.Index], chain);
        }
    }
}