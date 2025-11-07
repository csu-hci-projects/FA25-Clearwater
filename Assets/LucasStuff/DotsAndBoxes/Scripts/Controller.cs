using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace DotsAndBoxes
{
    public class Controller : MonoBehaviour
    {
        private static readonly WaitForSeconds waitForSeconds = new(0.5f);
        [SerializeField] CinemachineCamera mainCam;
        [SerializeField] Camera gameCam;
        private GameBoard gameBoard;
        private HeuristicAI AI;
        private Dictionary<Edge, EdgeRunner> edgeRunners;
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
                edgeRunners[er.edge] = er;
            }

            activePlayer = Player.Human;
        }

        public bool TryMove(Edge edge, Player player)
        {
            StartCoroutine(AITurn());

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

        private IEnumerator AITurn()
        {
            yield return waitForSeconds;

            Edge move = AI.ChooseMove(gameBoard);
            bool successful = TryMove(move, Player.AI);
            Debug.Log(successful);
            if (successful)
            {
                edgeRunners[move].AISet();
            }
        }
    }
}
