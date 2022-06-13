using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundCheck : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter musica;
    public static bool checkMute;

    public void Awake()
    {
        musica = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        Invoke("CheckMute", 0.5f);
    }

    private void OnEnable()
    {
        Invoke("CheckMute", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkMute)
        {
            if (PlayerPrefs.GetInt("mute", 1) == 0) musica.Stop();
            else if (PlayerPrefs.GetInt("mute", 1) == 1) musica.Play();      //.Play(); começa a tocar a música do ínicio
            checkMute = false;
        }
    }

    public void CheckMute()
    {
        if (PlayerPrefs.GetInt("mute", 1) == 0 && this.isActiveAndEnabled) musica.Stop();
        else if (PlayerPrefs.GetInt("mute", 1) == 1 && this.isActiveAndEnabled) musica.Play();      //.Play(); começa a tocar a música do ínicio
    }
}
