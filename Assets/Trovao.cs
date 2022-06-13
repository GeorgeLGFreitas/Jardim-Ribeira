using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trovao : MonoBehaviour
{
    public void PlaySound() { if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Trovão - Oneshot"); }
    public void DeactiveObject() { this.gameObject.SetActive(false); }
}
