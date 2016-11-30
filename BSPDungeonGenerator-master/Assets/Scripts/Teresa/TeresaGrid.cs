using UnityEngine;
using System.Collections;

public class TeresaGrid : MonoBehaviour {

    public int gridWidth { get; private set; }
    public int gridHeight { get; private set; }

    public int[,] grid { get; private set; }
    
    public int this[int _x, int _y] { set { grid[_x, _y] = value; } get { return grid[_x, _y]; } }


    public TeresaGrid()
    {
        grid = new int[gridWidth, gridHeight];

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                grid[i, j] = 0;
            }
        }
    }

    
}
