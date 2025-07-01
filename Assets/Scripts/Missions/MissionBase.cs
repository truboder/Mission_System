using System;
using UnityEngine;

namespace Missions
{
    public abstract class MissionBase : ScriptableObject, IMission
    {
        [SerializeField] private string missionName;
        [SerializeField] private float startDelaySeconds;
        [SerializeField] private MissionBase nextMission;

        public event Action OnStarted;
        public event Action OnMissionPointReached;
        public event Action OnFinished;

        public string MissionName => missionName;
        public float StartDelaySeconds => startDelaySeconds;
        public MissionBase NextMission => nextMission;

        public void Start()
        {
            OnStarted?.Invoke();
            ExecuteMission();
        }

        protected abstract void ExecuteMission();

        protected void CompleteMission()
        {
            OnFinished?.Invoke();
        }

        protected void TriggerMissionPoint()
        {
            OnMissionPointReached?.Invoke();
        }
    }
}