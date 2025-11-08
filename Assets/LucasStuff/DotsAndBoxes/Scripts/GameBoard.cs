using System;
using System.Collections.Generic;
using System.Linq;

namespace DotsAndBoxes
{
    public class GameBoard
    {
        private readonly int Rows = 6;
        private readonly int Columns = 6;
        private readonly Player[,] HorizontalEdges;
        private readonly Player[,] VerticalEdges;
        private readonly Player[,] Boxes;

        public GameBoard()
        {
            HorizontalEdges = new Player[Rows, Columns];
            VerticalEdges = new Player[Rows, Columns];
            Boxes = new Player[Rows - 1, Columns - 1];
        }

        public bool ApplyMove(Edge edge, Player player)
        {
            bool completed = false;

            if (edge.Type == EdgeType.Horizontal)
            {
                if (HorizontalEdges[edge.Row, edge.Column] != Player.None)
                    throw new InvalidOperationException($"Horizontal edge already exists at ({edge.Row}, {edge.Column})");
                HorizontalEdges[edge.Row, edge.Column] = player;
            }
            else
            {
                if (VerticalEdges[edge.Row, edge.Column] != Player.None)
                    throw new InvalidOperationException($"Vertical edge already exists at ({edge.Row}, {edge.Column})");
                VerticalEdges[edge.Row, edge.Column] = player;
            }

            foreach (var (r, c) in GetAffectedBoxes(edge))
            {
                if (r >= 0 && r < Rows - 1 && c >= 0 && c < Columns - 1)
                {
                    if (Boxes[r, c] == Player.None && IsBoxComplete(r, c))
                    {
                        Boxes[r, c] = player;
                        completed = true;
                    }
                }
            }

            return completed;
        }

        public IEnumerable<Edge> GetAvailableMoves()
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns - 1; c++)
                    if (HorizontalEdges[r, c] == Player.None)
                        yield return new Edge(EdgeType.Horizontal, r, c);

            for (int r = 0; r < Rows - 1; r++)
                for (int c = 0; c < Columns; c++)
                    if (VerticalEdges[r, c] == Player.None)
                        yield return new Edge(EdgeType.Vertical, r, c);
        }

        public IEnumerable<(int row, int col)> GetAffectedBoxes(Edge edge)
        {
            if (edge.Type == EdgeType.Horizontal)
            {
                if (edge.Row > 0)
                    yield return (edge.Row - 1, edge.Column);
                if (edge.Row < Rows - 1)
                    yield return (edge.Row, edge.Column);
            }
            else
            {
                if (edge.Column > 0)
                    yield return (edge.Row, edge.Column - 1);
                if (edge.Column < Columns - 1)
                    yield return (edge.Row, edge.Column);
            }
        }

        public bool HasEdge(int row, int col, EdgeType type, Edge? hypotheticalMove = null)
        {
            bool has;
            if (type == EdgeType.Horizontal)
            {
                if (row < 0 || row >= Rows || col < 0 || col >= Columns - 1)
                    return false;

                has = HorizontalEdges[row, col] != Player.None;
            }
            else
            {
                if (row < 0 || row >= Rows - 1 || col < 0 || col >= Columns)
                    return false;

                has = VerticalEdges[row, col] != Player.None;
            }

            if (hypotheticalMove.HasValue)
            {
                var h = hypotheticalMove.Value;
                if (h.Type == type && h.Row == row && h.Column == col)
                    has = true;
            }

            return has;
        }

        public bool IsGameOver()
        {
            return GetAvailableMoves().Count() == 0;
        }

        private bool CheckBoxes(bool isHorizontal, int row, int col, Player player)
        {
            bool completed = false;

            List<(int, int)> boxesToCheck = new();
            if (isHorizontal)
            {
                if (row > 0)
                    boxesToCheck.Add((row - 1, col));
                if (row < Rows - 1)
                    boxesToCheck.Add((row, col));
            }
            else
            {
                if (col > 0)
                    boxesToCheck.Add((row, col - 1));
                if (col < Columns)
                    boxesToCheck.Add((row, col));
            }

            foreach (var (r, c) in boxesToCheck)
            {
                if (r >= 0 && r < Rows - 1 && c >= 0 && c < Columns - 1)
                {
                    if (Boxes[r, c] == Player.None && IsBoxComplete(r, c))
                    {
                        Boxes[r, c] = player;
                        completed = true;
                    }
                }
            }

            return completed;
        }

        private bool IsBoxComplete(int row, int col)
        {
            return HorizontalEdges[row, col] != Player.None
                && HorizontalEdges[row + 1, col] != Player.None
                && VerticalEdges[row, col] != Player.None
                && VerticalEdges[row, col + 1] != Player.None;
        }
    }
}
