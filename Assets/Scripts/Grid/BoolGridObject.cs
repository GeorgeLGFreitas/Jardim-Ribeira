using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolGridObject
{
    private GridMap<BoolGridObject> grid;
    private int x;
    private int y;
    private bool b;

    public BoolGridObject(GridMap<BoolGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void True()
    {
        b = !b;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return b.ToString();
    }

    
}
