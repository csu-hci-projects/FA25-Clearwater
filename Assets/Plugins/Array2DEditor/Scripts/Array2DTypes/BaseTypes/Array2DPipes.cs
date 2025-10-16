using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DPipes : Array2D<Pipes>
    {
        [SerializeField]
        CellRowPipes[] cells = new CellRowPipes[Consts.defaultGridSize];

        protected override CellRow<Pipes> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
    
    [System.Serializable]
    public class CellRowPipes : CellRow<Pipes> { }
}