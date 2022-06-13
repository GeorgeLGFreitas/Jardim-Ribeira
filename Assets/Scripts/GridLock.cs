using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLock : MonoBehaviour
{
    GridMapConstructor gmc;
    bool lockB;

    // Start is called before the first frame update
    void Start()
    {
        lockB = true;
        gmc = FindObjectOfType<GridMapConstructor>();
    }


    // Update is called once per frame
    void Update()
    {
        if(lockB)
        {
            Lock();
            StartCoroutine(Deleta());
            lockB = false;
        }
       
        
    }

    public IEnumerator Deleta()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    void Lock()
    {
        HeatMapGridObject heatMapGridObject = gmc.grid.GetGridObject(transform.position);
        heatMapGridObject.AddValue(5);
    }


}
