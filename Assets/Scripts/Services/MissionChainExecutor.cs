using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Missions;

namespace Services
{
    public class MissionChainExecutor : MonoBehaviour
    {
        private const int SecondsToMilliseconds = 1000;

        private readonly Dictionary<MissionChain, ActiveChain> _activeChains = new Dictionary<MissionChain, ActiveChain>();
        private readonly Dictionary<MissionBase, Timer> _missionTimers = new Dictionary<MissionBase, Timer>();

        private void Start()
        {
            var missionService = ServiceLocator.Instance.GetService<MissionService>();
            missionService.ChainStarted += OnChainStarted;
        }

        private void OnDestroy()
        {
            var missionService = ServiceLocator.Instance.GetService<MissionService>();
            missionService.ChainStarted -= OnChainStarted;
        }

        private void OnChainStarted(MissionChain chain, Action onChainCompleted)
        {
            if (!_activeChains.ContainsKey(chain))
            {
                var activeChain = new ActiveChain { Chain = chain, Index = 0, OnCompleted = onChainCompleted };
                _activeChains[chain] = activeChain;
                StartMission(chain.Missions[0], chain, onChainCompleted);
            }
        }

        private async void StartMission(MissionBase mission, MissionChain chain, Action onChainCompleted)
        {
            Timer timer = new Timer();
            _missionTimers[mission] = timer;

            if (mission.StartDelaySeconds > 0)
            {
                await timer.StartAsync((int)(mission.StartDelaySeconds * SecondsToMilliseconds));
            }

            mission.OnFinished += () => OnMissionCompleted(mission, chain, onChainCompleted);
            mission.Start();
        }

        private void OnMissionCompleted(MissionBase mission, MissionChain chain, Action onChainCompleted)
        {
            _missionTimers.Remove(mission);

            if (_activeChains.TryGetValue(chain, out ActiveChain activeChain))
            {
                if (activeChain.Index < chain.Missions.Length - 1)
                {
                    activeChain.Index++;
                    _activeChains[chain] = activeChain;
                    StartMission(chain.Missions[activeChain.Index], chain, onChainCompleted);
                }
                else
                {
                    _activeChains.Remove(chain);
                    onChainCompleted?.Invoke();
                }
            }
        }
    }
}