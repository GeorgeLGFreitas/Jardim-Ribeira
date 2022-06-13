using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poço : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        CasaTeste casa = col.GetComponent<CasaTeste>();

        if(casa != null)
        {
            casa.poço = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        CasaTeste casa = col.GetComponent<CasaTeste>();

        if (casa != null)
        {
            casa.poço = false;
        }
    }
}
