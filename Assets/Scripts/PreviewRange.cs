using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewRange : MonoBehaviour
{
    public ObjectOnScene objOnScene;

    private void OnTriggerEnter2D(Collider2D col)
    {
        CasaTeste casa = col.GetComponent<CasaTeste>();
        if(casa != null && (objOnScene.GetActualTool() == objOnScene.poço || objOnScene.GetActualTool() == objOnScene.waterAir))
        {
            casa.SetWaterIndicator(true);
        }

        if(casa !=null && objOnScene.GetActualTool() == objOnScene.lixeira)
        {
            casa.SetEsgotoIndicator(true);
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        CasaTeste casa = col.GetComponent<CasaTeste>();
        if (casa != null)
        {
            casa.SetWaterIndicator(false);
            casa.SetEsgotoIndicator(false);
        }
    }
}
