using System;
using System.Collections.Generic;
using Missions;
using UnityEngine;

namespace Services
{
    public class MissionSystem : MonoBehaviour
    {
        private const int SecondsToMilliseconds = 1000;

        private readonly Dictionary<MissionBase, Timer> missionTimers = new Dictionary<MissionBase, Timer>();

        public void StartChain(MissionChain chain, Action onChainCompleted)
        {
            if (chain.Missions.Length == 0)
            {
                return;
            }

            StartMission(chain.Missions[0], onChainCompleted);
        }

        private async void StartMission(MissionBase mission, Action onChainCompleted)
        {
            Timer timer = new Timer();
            missionTimers[mission] = timer;

            if (mission.StartDelaySeconds > 0)
            {
                await timer.StartAsync((int)(mission.StartDelaySeconds * SecondsToMilliseconds));
            }

            mission.OnFinished += () => OnMissionFinished(mission, onChainCompleted);
            mission.Start();
        }

        private void OnMissionFinished(MissionBase mission, Action onChainCompleted)
        {
            missionTimers.Remove(mission);

            var nextMission = mission.NextMission;
            if (nextMission != null)
            {
                StartMission(nextMission, onChainCompleted);
            }
            else
            {
                onChainCompleted?.Invoke();
            }
        }
    }
}