using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GridMap<TGridObject>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs 
    {
        public int x;
        public int y;
    }

    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;
    

    private int width;
    private int height;
    public Vector3 originPosition; //valor da posição do "quadrado" em x e y
    public float cellSize;         //tamanho de cada tile
    private TGridObject[,] gridArray;      //armazenar o valor 
    private TextMesh[,] debugTextArray;
    public int i;
    public int y;
    

    
    // Gera o GridMap
    public GridMap(int width, int height, float cellSize, Vector3 originPosition, Func<GridMap<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for(i = 0; i < gridArray.GetLength(0); i++)
            {
            for (y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[i, y] = createGridObject(this, i, y);
                
            }

        }
        bool showDebug = false;
        if (showDebug)
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (i = 0; i < gridArray.GetLength(0); i++)
            {
                for (y = 0; y < gridArray.GetLength(1); y++)
                {

                    debugTextArray[i, y] = WorldText.CreateWorldText(gridArray[i, y]?.ToString(), null, GetWorldPosition(i, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);

                    Debug.DrawLine(GetWorldPosition(i, y), GetWorldPosition(i, y + 1), Color.white, 100f); // desenha as linhas brancas
                    Debug.DrawLine(GetWorldPosition(i, y), GetWorldPosition(i + 1, y), Color.white, 100f); // desenha as linhas brancas
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f); // desenha as linhas brancas
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f); // desenha as linhas brancas

            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition (int i, int y)
    {
        return new Vector3(i, y) * cellSize + originPosition;
    }

    public void GetXY (Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

    }
    public Vector3 GetXY2(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        return new Vector3(x, y);

    }


    //Coloca um valor no tile
    public void SetGridObject(int i, int y, TGridObject value)
    {
        if (i >= 0 && y >= 0 && i < width && y < height)
        {
            gridArray[i, y] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = i, y = y });
        }
    }

    //Atualiza o Objeto
    public void TriggerGridObjectChanged(int i, int y)
    {

        if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = i, y = y });

    }

    //Coloca um valor no tile parte II
    public void SetGridObject (Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    //Lê o valor de um tile
    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            //Debug.Log(gridArray[x, y]);
            return gridArray[x, y];    
        }
        else
        {
            return default(TGridObject);
        }
    }

    //Lê o valor de um tile parte II
    public TGridObject GetGridObject (Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        //Debug.Log(GetGridObject(x, y));
        return GetGridObject(x, y);
    }
    
    //teste
    public Vector2 GetCellPosition(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return new Vector2(x, y);


    }
 
}
