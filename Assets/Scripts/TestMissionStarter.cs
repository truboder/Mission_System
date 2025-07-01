using Missions;
using Services;
using UnityEngine;

public class TestMissionStarter : MonoBehaviour
{
    [SerializeField] private MissionChain testChain1;
    [SerializeField] private MissionChain testChain2;
    private MissionService missionService;

    private void Start()
    {
        missionService = FindObjectOfType<MissionService>();
        if (missionService != null)
        {
            if (testChain1 != null) missionService.StartChain(testChain1);
            if (testChain2 != null) missionService.StartChain(testChain2);
        }
        else
        {
            Debug.LogError("MissionService not found!");
        }
    }
}