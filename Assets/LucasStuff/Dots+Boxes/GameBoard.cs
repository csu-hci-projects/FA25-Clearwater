enum EdgeType { Horizontal, Vertical }

struct Edge
{
    public EdgeType Type;
    public int X;
    public int Y;
}

public class GameBoard
{
    private int rows;
    private int cols;
    private bool[,,] edges;
    private int[,] boxes;

    public GameBoard(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        edges = new bool[rows + 1, cols + 1, 2];
        boxes = new int[rows, cols];

        for (int row = 0; row < rows; ++row)
            for (int col = 0; col < cols; ++col)
                boxes[row, col] = -1;
    }

    public bool IsEdgeAvailable(Edge e)
    {
        return !edges[e.Y, e.X, e.Type == EdgeType.Horizontal ? 0 : 1];
    }

    public void ApplyMove(Edge e, int playerID)
    {
        edges[e.Y, e.X, e.Type == EdgeType.Horizontal ? 0 : 1] = true;
    }
}
