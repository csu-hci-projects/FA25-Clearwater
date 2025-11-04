using Unity.Cinemachine;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] CinemachineCamera mainCam;
    [SerializeField] Camera gameCam;

    void Awake()
    {
        mainCam.enabled = true;
        gameCam.enabled = false;
    }

    void Update()
    {

    }
}
