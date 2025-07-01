using System;
using System.Collections.Generic;
using Missions;
using UnityEngine;

namespace Services
{
    public class MissionSystem : MonoBehaviour
    {
        private readonly Dictionary<MissionBase, MissionChain> missionToChainMap = new Dictionary<MissionBase, MissionChain>();
        private readonly Dictionary<MissionBase, Timer> missionTimers = new Dictionary<MissionBase, Timer>();

        public void StartChain(MissionChain chain, Action onChainCompleted)
        {
            if (chain.Missions.Length == 0) return;

            foreach (var mission in chain.Missions)
            {
                missionToChainMap[mission] = chain;
            }

            StartMission(chain.Missions[0], onChainCompleted);
        }

        private async void StartMission(MissionBase mission, Action onChainCompleted)
        {
            if (mission == null) return;

            Timer timer = new Timer();
            missionTimers[mission] = timer;

            mission.OnFinished += () => OnMissionFinished(mission, onChainCompleted);

            if (mission.StartDelaySeconds > 0)
            {
                await timer.StartAsync((int)(mission.StartDelaySeconds * 1000));
            }

            if (missionTimers.ContainsKey(mission))
            {
                mission.Start();
            }
        }

        private void OnMissionFinished(MissionBase mission, Action onChainCompleted)
        {
            missionTimers.Remove(mission);
            missionToChainMap.Remove(mission);

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