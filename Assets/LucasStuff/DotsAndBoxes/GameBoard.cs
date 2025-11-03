enum Player { None, Human, AI }

struct Edge
{
    public EdgeType Type;
    public int X;
    public int Y;
}

public class GameBoard
{
    private readonly int rows;
    private readonly int cols;
    private readonly bool[,,] edges;
    private readonly Player[,] boxes;

    public GameBoard(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        edges = new bool[rows + 1, cols + 1, 2];
        boxes = new int[rows, cols];

        for (int row = 0; row < rows; ++row)
            for (int col = 0; col < cols; ++col)
                boxes[row, col] = Player.None;
    }

    public bool IsEdgeAvailable(Edge e)
    {
        return !edges[e.Y, e.X, e.Type];
    }

    public void ApplyMove(Edge e, Player player)
    {
        edges[e.Y, e.X, e.Type] = true;

        foreach (var (row, col) in GetAffectedBoxes(e))
            if (IsBoxCompleted(row, col))
                boxes[row, col] = player;
    }

    private bool IsBoxCompleted(int row, int col)
    {
        bool top = edges[row, col, EdgeType.Horizontal];
        bool bottom = edges[row + 1, col, EdgeType.Horizontal];
        bool left = edges[row, col, EdgeType.Vertical];
        bool right = edges[row, col + 1, EdgeType.Vertical];
        return top && bottom && left && right;
    }

    private static List<(int row, int col)> GetAffectedBoxes(Edge e)
    {
        List<(int, int)> list = [];

        if (e.Type == EdgeType.Horizontal)
        {
            if (e.Y < rows) list.Add((e.Y, e.X));
            if (e.Y > 0) list.Add((e.Y - 1, e.X));
        }
        else
        {
            if (e.X < cols) list.Add((e.Y, e.X));
            if (e.X > 0) list.Add((e.Y, e.X - 1));
        }

        return list;
    }

    public IEnumerable<Edge> GetAvailableMoves()
    {
        for (int y = 0; y <= rows; ++y)
            for (int x = 0; x < cols; x++)
                if (!edges[y, x, EdgeType.Horizontal])
                    yield return new Edge { Type = EdgeType.Horizontal, X = x, Y = y };

        for (int y = 0; y < rows; y++)
            for (int x = 0; x <= cols; x++)
                if (!edges[y, x, EdgeType.Vertical])
                    yield return new Edge { Type = EdgeType.Vertical, X = x, Y = y };
    }
}
