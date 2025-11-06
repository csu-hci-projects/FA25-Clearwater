using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace DotsAndBoxes
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] CinemachineCamera mainCam;
        [SerializeField] Camera gameCam;
        private GameBoard gameBoard;
        private bool gaming;
        private Dictionary<(EdgeType, int, int), EdgeRunner> edgeRunners = new();

        void Awake()
        {
            mainCam.tag = "Untagged";
            gameCam.tag = "MainCamera";
            mainCam.enabled = false;
            gameCam.enabled = true;

            gaming = false;
        }

        void OnEnable()
        {
            gameBoard = new();

            foreach (var er in FindObjectsByType<EdgeRunner>(FindObjectsSortMode.None))
            {
                er.Init(this);
                edgeRunners[(er.Type, er.X, er.Y)] = er;
            }
        }

        void Update()
        {

        }
    }
}
