using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteNextRound : MonoBehaviour
{
    public WaveScore waveScore;
    public GameObject blockNextRoundButton;

    public void BloqueiaBotao() { blockNextRoundButton.SetActive(true); }
    public void NextRound()
    { 
        waveScore.NextRound();
         
    }
    public void LigaCoisas()
    { 
        waveScore.LigaCoisas();
        blockNextRoundButton.SetActive(false);
    }
    public void DeactivateObject() { this.gameObject.SetActive(false); }
}
