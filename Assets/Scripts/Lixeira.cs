using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixeira : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        CasaTeste casa = col.GetComponent<CasaTeste>();

        if (casa != null)
        {
            casa.lixeira = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        CasaTeste casa = col.GetComponent<CasaTeste>();

        if (casa != null)
        {
            casa.lixeira = false;
        }
    }
}
