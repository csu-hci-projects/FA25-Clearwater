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
        private HeuristicAI AI;
        private Dictionary<(EdgeType, int, int), EdgeRunner> edgeRunners;
        private Player activePlayer;
        // private bool gaming;

        void Awake()
        {
            gameBoard = new();
            AI = new();
            edgeRunners = new();

            mainCam.tag = "Untagged";
            gameCam.tag = "MainCamera";
            mainCam.enabled = false;
            gameCam.enabled = true;

            // gaming = true;
        }

        void Start()
        {
            foreach (var er in FindObjectsByType<EdgeRunner>(FindObjectsSortMode.None))
            {
                string[] parts = er.name.Split('-');  // fmt: Edge-R-C
                er.Init(this, int.Parse(parts[1]), int.Parse(parts[2]));
                edgeRunners[(er.edge.Type, er.edge.Row, er.edge.Column)] = er;
            }

            activePlayer = Player.Human;
        }

        public bool TryMove(Edge edge, Player player)
        {
            if (!gameBoard.HasEdge(edge.Row, edge.Column, edge.Type) && player == activePlayer)
            {
                bool boxCompleted = gameBoard.ApplyMove(edge, player);
                if (!boxCompleted)
                {
                    activePlayer = activePlayer == Player.Human ? Player.AI : Player.Human;
                }

                return true;
            }

            return false;
        }
    }
}
