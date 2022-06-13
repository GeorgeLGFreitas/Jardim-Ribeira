using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TesteTileMap : MonoBehaviour
{
    [SerializeField] private TileMapVisual tilemapVisual;
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;

    public Camera worldCamera;

    private void Start()
    {
        tilemap = new Tilemap(14, 7, 10f, Vector3.zero);

        tilemap.SetTilemapVisual(tilemapVisual);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Vector3 touchVec3 = worldCamera.ScreenToWorldPoint(touch.position);
                tilemap.SetTilemapSprite(touchVec3, tilemapSprite);
            }


        }

       
    }

    public void ChangeTile(string tileName)
    {
        switch (tileName)
        {
            case "Mato1":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato1;
                break;

            case "Mato2":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato2;
                break;

            case "Mato3":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato3;
                break;

            case "Mato4":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato4;
                break;

            case "Terra":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Terra;
                break;

            case "RioBD":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioBD;
                break;

            case "RioBB":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioBB;
                break;

            case "RioBE":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioBE;
                break;

            case "RioBC":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioBC;
                break;

            case "RioH":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioH;
                break;

            case "RioV":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioV;
                break;

            case "RioCD":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioCD;
                break;

            case "RioCB":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioCB;
                break;

            case "RioCE":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioCE;
                break;

            case "RioCC":
                tilemapSprite = Tilemap.TilemapObject.TilemapSprite.RioCC;
                break;

        }
    }
}
