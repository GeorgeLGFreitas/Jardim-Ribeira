using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilemaker : MonoBehaviour
{

    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SetTile()
    {

        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mato1;
        tilemap.SetTilemapSprite(this.transform.position, tilemapSprite);
    }
}
