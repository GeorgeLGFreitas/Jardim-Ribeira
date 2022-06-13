using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap : MonoBehaviour
{
    public event EventHandler OnLoaded;
    private GridMap<TilemapObject> grid;

    public Tilemap(int width, int height, float cellSize, Vector3 originPosition)
    {
        grid = new GridMap<TilemapObject>(width, height, cellSize, originPosition, (GridMap<TilemapObject> g, int x, int y) => new TilemapObject(g, x, y));
    }


    //Coloca a Sprite no Tile
    public void SetTilemapSprite(Vector3 worldPosition, TilemapObject.TilemapSprite tilemapSprite)
    {
        TilemapObject tilemapObject = grid.GetGridObject(worldPosition);
        if(tilemapObject != null)
        {
            tilemapObject.SetTilemapSprite(tilemapSprite);
        }
    }

    public void Save()
    {
        List<TilemapObject.SaveObject> tilemapObjectSaveObjectList = new List<TilemapObject.SaveObject>();
        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                TilemapObject tilemapObject = grid.GetGridObject(x, y);
                tilemapObjectSaveObjectList.Add(tilemapObject.Save());
            }
        }
        SaveObject saveObject = new SaveObject { tilemapObjetcSaveObjectArray = tilemapObjectSaveObjectList.ToArray() };

        SaveSystem.SaveObject(saveObject);
    }

    public void Load()
    {
        SaveObject saveObject = SaveSystem.LoadMostRecentObject<SaveObject>();
        foreach(TilemapObject.SaveObject tilemapObjectSaveObject in saveObject.tilemapObjetcSaveObjectArray)
        {
            TilemapObject tilemapObject = grid.GetGridObject(tilemapObjectSaveObject.x, tilemapObjectSaveObject.y);
            tilemapObject.Load(tilemapObjectSaveObject);
        }
        OnLoaded?.Invoke(this, EventArgs.Empty);
    }

    public void Load(string save)
    {
        SaveObject saveObject = SaveSystem.LoadtObject<SaveObject>(save);
        foreach (TilemapObject.SaveObject tilemapObjectSaveObject in saveObject.tilemapObjetcSaveObjectArray)
        {
            TilemapObject tilemapObject = grid.GetGridObject(tilemapObjectSaveObject.x, tilemapObjectSaveObject.y);
            tilemapObject.Load(tilemapObjectSaveObject);
        }
        OnLoaded?.Invoke(this, EventArgs.Empty);
    }



    public class SaveObject
    {
        public TilemapObject.SaveObject[] tilemapObjetcSaveObjectArray;
    }

    public void SetTilemapVisual(TileMapVisual tilemapVisual)
    {
        tilemapVisual.SetGrid(this,grid);
    }


    public class TilemapObject
    {
        public enum TilemapSprite
        {
            None,
            Mato1,
            Mato2,
            Mato3,
            Mato4,
            Terra,
            RioBD,
            RioBB,
            RioBE,
            RioBC,
            RioH,
            RioV,
            RioCD,
            RioCB,
            RioCE,
            RioCC,
        }

        private GridMap<TilemapObject> grid;
        private int x;
        private int y;
        private TilemapSprite tilemapSprite;


        public TilemapObject(GridMap<TilemapObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetTilemapSprite(TilemapSprite tilemapSprite)
        {
            this.tilemapSprite = tilemapSprite;
            grid.TriggerGridObjectChanged(x, y);
        }


        public TilemapSprite GetTilemapSprite()
        {
            return tilemapSprite;
        }


        public override string ToString()
        {
            return tilemapSprite.ToString();
        }


        [System.Serializable]
        public class SaveObject
        {
            public TilemapSprite tilemapSprite;
            public int x;
            public int y;
        }

        public SaveObject Save()
        {
            return new SaveObject
            {
                tilemapSprite = tilemapSprite,
                x = x,
                y = y,
            };
        }

        public void Load(SaveObject saveObject)
        {
            tilemapSprite = saveObject.tilemapSprite;
        }
    }
}

