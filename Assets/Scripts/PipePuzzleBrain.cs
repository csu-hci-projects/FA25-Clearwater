using System;
using Unity.VisualScripting;
using UnityEngine;
using Array2DEditor;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class PipePuzzleBrain : MonoBehaviour, IsToggleable
{
    private bool finished = false;
    enum Rotation { UP, RIGHT, DOWN, LEFT }
    [SerializeField] GameObject pipeD;
    [SerializeField] GameObject pipeL;
    [SerializeField] GameObject pipeI;
    [SerializeField] GameObject pipeT;
    [SerializeField] Array2DPipes insepctorPipesGrid;
    [SerializeField] int pipeSize;

    private (Pipes type, Rotation rotation)[,] dataGrid;
    private GameObject[,] pipesGrid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataGrid = new (Pipes type, Rotation rotation)[insepctorPipesGrid.GridSize.y, insepctorPipesGrid.GridSize.x];
        pipesGrid = new GameObject[insepctorPipesGrid.GridSize.y, insepctorPipesGrid.GridSize.x];
        for (int i = 0; i < insepctorPipesGrid.GridSize.y; i++)
        {
            for (int j = 0; j < insepctorPipesGrid.GridSize.x; j++)
            {
                dataGrid[i, j] = (insepctorPipesGrid.GetCell(i, j), (Rotation)UnityEngine.Random.Range(0, 3));
                Vector3 pipePosition = transform.position + new Vector3(i * pipeSize, 0, j * pipeSize * -1);
                switch (insepctorPipesGrid.GetCell(i, j))
                {
                    case Pipes.D:
                        pipesGrid[i, j] = Instantiate(pipeD, pipePosition, Quaternion.identity, transform);
                        break;
                    case Pipes.L:
                        pipesGrid[i, j] = Instantiate(pipeL, pipePosition, Quaternion.identity, transform);
                        break;
                    case Pipes.I:
                        pipesGrid[i, j] = Instantiate(pipeI, pipePosition, Quaternion.identity, transform);
                        break;
                    case Pipes.T:
                        pipesGrid[i, j] = Instantiate(pipeT, pipePosition, Quaternion.identity, transform);
                        break;
                }
                pipesGrid[i, j].transform.Rotate(0, 90f * (int)dataGrid[i, j].rotation, 0);
                BePipe currentPipe = pipesGrid[i, j].GetComponent<BePipe>();
                currentPipe.SetGridPosition(i, j);
            }
        }
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard.tKey.IsPressed())
        {
            finished = CheckCompletion();
            if (finished) Debug.Log("The Puzzle is Solved!");
            else Debug.Log("The Puzzle is Not Solved!");
        }

    }

    public void RotatePipe(int row, int column)
    {
        dataGrid[row, column].rotation = (Rotation)(((int)dataGrid[row, column].rotation + 1) % 4);
        pipesGrid[row, column].transform.Rotate(0, 90f, 0);
    }

    bool CheckCompletion()
    {
        List<Rotation>[,] connections = InitializeConnections(insepctorPipesGrid.GridSize.y, insepctorPipesGrid.GridSize.x);
        for (int i = 0; i < connections.GetLength(0); i++)
        {
            for (int j = 0; j < connections.GetLength(1); j++)
            {
                if (connections[i, j].Contains(Rotation.UP))
                {
                    if (j == 0 || !connections[i, j - 1].Contains(Rotation.DOWN)) return false;
                }
                if (connections[i, j].Contains(Rotation.RIGHT))
                {
                    if (i == connections.GetLength(0) - 1 || !connections[i + 1, j].Contains(Rotation.LEFT)) return false;
                }
                if (connections[i, j].Contains(Rotation.DOWN))
                {
                    if (j == connections.GetLength(1) - 1 || !connections[i, j + 1].Contains(Rotation.UP)) return false;
                }
                if (connections[i, j].Contains(Rotation.LEFT))
                {
                    if (i == 0 || !connections[i - 1, j].Contains(Rotation.RIGHT)) return false;
                }
            }
        }
        return true;
    }

    List<Rotation>[,] InitializeConnections(int height, int width)
    {
        List<Rotation>[,] connections = new List<Rotation>[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Rotation[] pipeOpenings = { };
                connections[i, j] = new List<Rotation>();
                switch (dataGrid[i, j].type)
                {
                    case Pipes.D:
                        pipeOpenings = new Rotation[] { Rotation.UP };
                        break;
                    case Pipes.L:
                        pipeOpenings = new Rotation[] { Rotation.UP, Rotation.RIGHT };
                        break;
                    case Pipes.I:
                        pipeOpenings = new Rotation[] { Rotation.UP, Rotation.DOWN };
                        break;
                    case Pipes.T:
                        pipeOpenings = new Rotation[] { Rotation.UP, Rotation.RIGHT, Rotation.DOWN };
                        break;
                }
                for (int k = 0; k < pipeOpenings.Length; k++)
                {
                    connections[i, j].Add((Rotation)(((int)pipeOpenings[k] + (int)dataGrid[i, j].rotation) % 4));
                }
            }
        }
        return connections;
    }

    public void OnToggle()
    {
        //Feedback for completing the puzzle is still necessary
        if (CheckCompletion()) Debug.Log("Puzzle Complete!");
    }
}
