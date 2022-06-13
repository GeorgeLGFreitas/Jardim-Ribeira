using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GridMapConstructor : MonoBehaviour
{
    //[SerializeField] private HeatMapVisual heatMapVisual;
    //[SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;
    [SerializeField] private TileMapVisual tileMapVisual;

    //private Tilemap tilemap;
    //private Tilemap.TilemapObject.TilemapSprite tilemapSprite;

    public GridMap<HeatMapGridObject> grid;
    public GridMap<HeatMapGridObject> tileGrid;
    public GridMap<StringGridObject> stringGrid;
    public GridMap<BoolGridObject> boolGrid;
    


  

    //coisas de criar na cena


    private void Start()
    {
    
        

       // tilemap = new Tilemap(15, 10, 10f, Vector3.zero);
        //tilemap.SetTilemapVisual(tileMapVisual);

        grid = new GridMap<HeatMapGridObject>(17, 8, 10f, new Vector3(10f, 10f, 0), (GridMap<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x , y));
        //tileGrid = new GridMap<HeatMapGridObject>(15, 10, 10f, Vector3.zero, (GridMap<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        //stringGrid = new GridMap<StringGridObject>(14, 8, 10f, Vector3.zero, (GridMap<StringGridObject> g, int x, int y) => new StringGridObject(g, x, y));
        //boolGrid = new GridMap<BoolGridObject>(14, 8, 10f, Vector3.zero, (GridMap<BoolGridObject> g, int x, int y) => new BoolGridObject(g, x, y));

        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);
        heatMapGenericVisual.SetGrid(grid);
        //tilemap.Load();
        //Debug.Log(Application.persistentDataPath);
        //Debug.Log(Application.streamingAssetsPath);
        //Debug.Log(Application.dataPath);
        //Debug.Log(SaveSystem.SAVE_FOLDER);
        //tilemap.Load();

      
    }

    void Update()
    {

        /*
        Vector3 mousePosition = MousePosition.GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            
            tilemap.SetTilemapSprite(mousePosition, tilemapSprite);
            
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato1;
           
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato2;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato3;

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato4;

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Terra;

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioBD;

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioBB;

        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioBE;

        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioBC;

        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioH;

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioV;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioCD;

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioCB;

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioCE;

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioCC;

        }



        if (Input.GetKeyDown(KeyCode.P))
        {
            tilemap.Save();

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            tilemap.Load();

        }
        */

        /*
        if (waveS.actualRound >= 2 && loadteste)
        {
            loadteste = false;
            tilemap.Load("save_11");
        }

        if (waveS.actualRound >= 4 && loadteste2)
        {
            loadteste2 = false;
            tilemap.Load("save_10");
        }
        */

        
    }

   
   
   
}





