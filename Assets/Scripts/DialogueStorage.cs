using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStorage : MonoBehaviour
{
    public static DialogueStorage SharedInstance;
    public GameObject[] dialogues;

    private void Awake()
    {
        SharedInstance = this;
    }

    public void LigaDialogo(int indexArrayDialogue)
    {
        dialogues[indexArrayDialogue].SetActive(true);
    }
}
