namespace DotsAndBoxes
{
    public struct Edge
    {
        public EdgeType Type;
        public int Row;
        public int Column;

        public Edge(EdgeType type, int row, int col)
        {
            Type = type;
            Row = row;
            Column = col;
        }
    }
}
