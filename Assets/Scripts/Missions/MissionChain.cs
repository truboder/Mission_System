using UnityEngine;

namespace Missions
{
    [CreateAssetMenu(fileName = "NewMissionChain", menuName = "Missions/MissionChain")]
    public class MissionChain : ScriptableObject
    {
        [SerializeField] private MissionBase[] missions;

        public MissionBase[] Missions => missions;
    }
}