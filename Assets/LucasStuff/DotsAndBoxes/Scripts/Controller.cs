using Unity.Cinemachine;
using UnityEngine;

[System.Serializable]
public class GameObjectRow
{
    public GameObject[] elements;
}

public class Controller : MonoBehaviour
{
    [SerializeField] CinemachineCamera mainCam;
    [SerializeField] Camera gameCam;
    [SerializeField] GameObjectRow[] horizontalEdges;
    [SerializeField] GameObjectRow[] verticalEdges;
    private bool gaming;

    void Awake()
    {
        mainCam.tag = "Untagged";
        gameCam.tag = "MainCamera";
        mainCam.enabled = false;
        gameCam.enabled = true;

        gaming = false;
    }

    void Update()
    {

    }
}
