using UnityEngine;
using System.Collections;
using System;

public class TeresaGrid {

    public int gridWidth { get; private set; }
    public int gridHeight { get; private set; }

    public int[,] grid { get; private set; }
    
	public int this[int _x, int _y] { set { 
			Debug.Log (String.Format("Setting {0},{1}", _x, _y));
			grid[_x, _y] = value; } get { return grid[_x, _y]; } }


    public TeresaGrid(int gridWidth, int gridHeight)
    {
        //gridWidth = gridWidth;
        //gridHeight = gridHeight; 

        //grid = new int[gridWidth, gridHeight];

        //for (int i = 0; i < gridWidth; i++)
        //{
        //    for (int j = 0; j < gridHeight; j++)
        //    {
        //        grid[i, j] = 0;
        //    }
        //}
    }

    public void Inicialize()
    {
        gridHeight = 300;
        gridWidth = 300;

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
