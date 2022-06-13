using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private Image actualImage;
    public Sprite mutedOn, mutedFalse;
    private bool checou;

    private void Awake()
    {
        actualImage = GetComponent<Image>();
        checou = false;
    }

    private void Start()
    {
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (PlayerPrefs.GetInt("mute", 1) == 0) actualImage.sprite = mutedOn;
        else if (PlayerPrefs.GetInt("mute", 1) == 1) actualImage.sprite = mutedFalse;
    }

    public void Mute()
    {
        if (PlayerPrefs.GetInt("mute", 1) == 0)
        {
            PlayerPrefs.SetInt("mute", 1);          //0 = mutado . 1 = desmutado
            print("Desmutou!");
        }
        else if (PlayerPrefs.GetInt("mute", 1) == 1)
        {
            PlayerPrefs.SetInt("mute", 0);
            print("Mutou!");
        }

        ChangeSprite();

        SoundCheck.checkMute = true;

    }
}
