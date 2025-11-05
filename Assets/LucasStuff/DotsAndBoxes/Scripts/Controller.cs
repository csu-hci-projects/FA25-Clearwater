using Unity.Cinemachine;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] CinemachineCamera mainCam;
    [SerializeField] Camera gameCam;
    private bool gaming;

    void Awake()
    {
        mainCam.enabled = true;
        gameCam.enabled = false;
        gaming = false;
    }

    void Update()
    {

    }
}
