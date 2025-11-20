using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DotsAndBoxes
{
    public class Controller : MonoBehaviour
    {
        private static readonly WaitForSeconds waitForSeconds = new(0.5f);
        [SerializeField] CinemachineCamera mainCam;
        [SerializeField] Camera gameCam;
        [SerializeField] GameObject playerDetect;
        private GameBoard gameBoard;
        private HeuristicAI AI;
        private Dictionary<Edge, EdgeRunner> edgeRunners;
        private Dictionary<(int, int), BoxRunner> boxRunners;
        private Player activePlayer;
        private bool gaming;
        private DialogueUI dialogueUI;
        private bool textIsPrinting;

        void Awake()
        {
            boxRunners = new();
            gameBoard = new(boxRunners);
            AI = new();
            edgeRunners = new();

            dialogueUI = playerDetect.GetComponent<DialogueUI>();

            InputSystem.EnableDevice(Keyboard.current);
            mainCam.tag = "MainCamera";
            gameCam.tag = "Untagged";
            mainCam.enabled = true;
            gameCam.enabled = false;

            gaming = false;
            textIsPrinting = false;
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

        void Update()
        {
            if (!gaming)
            {
                if (dialogueUI.PlayerDetection && dialogueUI.DialogueRunning)
                {
                    textIsPrinting = true;
                } else if (dialogueUI.PlayerDetection && !dialogueUI.DialogueRunning && textIsPrinting)
                {
                    textIsPrinting = false;

                    mainCam.tag = "Untagged";
                    gameCam.tag = "MainCamera";
                    mainCam.enabled = false;
                    gameCam.enabled = true;

                    gaming = true;
                }
            }
        }

        public bool TryMove(Edge edge, Player player)
        {
            bool successful = false, gameOver = false;
            if (gaming)
            {
                if (!gameBoard.HasEdge(edge.Row, edge.Column, edge.Type) && player == activePlayer)
                {
                    bool boxCompleted = gameBoard.ApplyMove(edge, player);
                    if (!boxCompleted)
                    {
                        activePlayer = activePlayer == Player.Human ? Player.AI : Player.Human;
                    }

                    gameOver = gameBoard.IsGameOver();

                    if (!gameOver && activePlayer == Player.AI)
                        StartCoroutine(AITurn());

                    successful = true;
                }

                if (gameOver || gameBoard.IsGameOver())
                {
                    activePlayer = Player.None;

                    int humanScore, AIScore;
                    (humanScore, AIScore) = gameBoard.GetScores();

                    mainCam.tag = "MainCamera";
                    gameCam.tag = "Untagged";
                    mainCam.enabled = true;
                    gameCam.enabled = false;

                    gaming = false;
                }
            }

            return successful;
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
