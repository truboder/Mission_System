using Infrastructure;
using Missions;
using Services;
using UnityEngine;

public class TestMissionStarter : MonoBehaviour
{
    [SerializeField] private MissionChain testChain1;
    [SerializeField] private MissionChain testChain2;

    private void Start()
    {
        MissionService missionService = ServiceLocator.Instance.GetService<MissionService>();
        
        if (missionService == null)
        {
            return;
        }

        missionService.StartChain(testChain1);
        missionService.StartChain(testChain2);
    }
}