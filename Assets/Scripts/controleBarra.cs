using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controleBarra : MonoBehaviour
{
    public Slider aguaSlider, saudeSlider, poluiSlider;

    public void SetSaude(int saude)
    {
        saudeSlider.value = saude;
    }

    public void SetAgua(float agua)
    {
        aguaSlider.value = agua;
    }
    public void SetPolui(float polui)
    {
        poluiSlider.value = 100f - polui;
    }


}
