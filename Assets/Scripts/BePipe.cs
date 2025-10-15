using UnityEngine;

public class BePipe : MonoBehaviour, IsHittable
{
    private int row;
    private int column;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void SetGridPosition(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    [ContextMenu("Rotate")]
    public void RotatePipe()
    {
        try
        {
            PipePuzzleBrain puzzleBrain = transform.parent.GetComponent<PipePuzzleBrain>();
            puzzleBrain.RotatePipe(row, column);
        }
        catch
        {

        }
    }

    public void OnHit()
    {
        RotatePipe();
    }
}
