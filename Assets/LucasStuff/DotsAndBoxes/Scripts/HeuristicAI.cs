using System;

namespace DotsAndBoxes
{
    public class HeuristicAI
    {
        private readonly Random random = new();

        public Edge ChooseMove(GameBoard board)
        {
            Edge? bestMove = null;
            int bestScore = int.MinValue;

            foreach (var move in board.GetAvailableMoves())
            {
                int score = EvaluateMove(board, move);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove ?? throw new Exception("AI couldn't find a move!");
        }

        private int EvaluateMove(GameBoard board, Edge move)
        {
            int score = 0;

            foreach (var (row, col) in board.GetAffectedBoxes(move))
            {
                score += random.Next(-5, 6);
                switch (CountBoxEdges(board, row, col, move))
                {
                    case 1:
                        score += 5; break;
                    case 2:
                        score += 5; break;
                    case 3:
                        score -= 20; break;
                    case 4:
                        score += 30; break;
                }
            }

            return score;
        }

        private int CountBoxEdges(GameBoard board, int row, int col, Edge hypotheticalMove)
        {
            int count = 0;

            if (board.HasEdge(row, col, EdgeType.Horizontal, hypotheticalMove)) count++;
            if (board.HasEdge(row + 1, col, EdgeType.Horizontal, hypotheticalMove)) count++;
            if (board.HasEdge(row, col, EdgeType.Vertical, hypotheticalMove)) count++;
            if (board.HasEdge(row, col + 1, EdgeType.Vertical, hypotheticalMove)) count++;

            return count;
        }
    }
}
