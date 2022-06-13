using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerFinder : MonoBehaviour
{
    ObjectOnScene gameM;
    // Start is called before the first frame update

    void Start()
    {
        gameM = FindObjectOfType<ObjectOnScene>();
    }

    // Update is called once per frame

    public void Dele()
    {
        gameM = FindObjectOfType<ObjectOnScene>();
        gameM.Deleta();
    }

    
   
}
