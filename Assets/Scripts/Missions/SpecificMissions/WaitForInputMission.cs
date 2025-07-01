using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Missions.SpecificMissions
{
    [CreateAssetMenu(fileName = "NewWaitForInputMission", menuName = "Missions/WaitForInputMission")]
    public class WaitForInputMission : MissionBase
    {
        [SerializeField] private KeyCode keyToPress = KeyCode.Space;

        protected override async void ExecuteMission()
        {
            Debug.Log($"Mission {MissionName} started. Press {keyToPress} to complete.");

            await UniTask.WaitUntil(() => Input.GetKeyDown(keyToPress));
            TriggerMissionPoint();
            Debug.Log($"Mission {MissionName} point reached.");

            await UniTask.Delay(1000);
            CompleteMission();
            Debug.Log($"Mission {MissionName} completed.");
        }
    }
}