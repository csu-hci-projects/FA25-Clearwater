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
        private Dictionary<(int, int), BoxRunner> boxRunners;
        private Player activePlayer;
        // private bool gaming;

        void Awake()
        {
            boxRunners = new();
            gameBoard = new(boxRunners);
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
                string[] parts = er.name.Split('-');  // fmt: Edge-C-R
                er.Init(this, int.Parse(parts[2]), int.Parse(parts[1]));
                edgeRunners[er.edge] = er;
            }

            foreach (var box in FindObjectsByType<BoxRunner>(FindObjectsSortMode.None))
            {
                string[] parts = box.name.Split('-');  // fmt: Edge-C-R
                boxRunners[(int.Parse(parts[2]), int.Parse(parts[1]))] = box;
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

                if (activePlayer == Player.AI)
                    StartCoroutine(AITurn());

                return true;
            }

            return false;
        }

        private IEnumerator AITurn()
        {
            yield return waitForSeconds;

            Edge move = AI.ChooseMove(gameBoard);
            bool successful = TryMove(move, Player.AI);
            if (successful)
            {
                edgeRunners[move].AISet();
            }
        }
    }
}
