using System;
using System.Collections.Generic;

public class GameBoard
{
    private readonly int Rows = 6;
    private readonly int Columns = 6;
    private readonly Player[,] HorizontalEdges;
    private readonly Player[,] VerticalEdges;
    private readonly Player[,] Boxes;

    public GameBoard()
    {
        HorizontalEdges = new Player[Rows, Columns - 1];
        VerticalEdges = new Player[Rows - 1, Columns];
        Boxes = new Player[Rows - 1, Columns - 1];
    }

    /// <summary>
    /// Returns true if a box was completed
    /// </summary>
    public bool DrawLine(bool isHorizontal, int row, int col, Player player)
    {
        if (isHorizontal)
        {
            if (HorizontalEdges[row, col] != Player.None)
                throw new InvalidOperationException($"Horizontal line at ({row},{col}) already set");

            HorizontalEdges[row, col] = player;
        }
        else
        {
            if (VerticalEdges[row, col] != Player.None)
                throw new InvalidOperationException($"Vertical line at ({row},{col}) already set");

            VerticalEdges[row, col] = player;
        }

        return CheckBoxes(isHorizontal, row, col, player);
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
