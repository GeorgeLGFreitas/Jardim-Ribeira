using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObject : MonoBehaviour
{
    public void SetObjectActive(string booleana)
    { 
        switch(booleana)
        {
            case "true":
                this.gameObject.SetActive(true);
                break;

            case "false":
                this.gameObject.SetActive(false);
                break;
        }
        
    }
}
