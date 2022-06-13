using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menus : MonoBehaviour
{
    public static UI_Menus uiMenus;
    public GameObject mainStore, mainStore2, store1, store2, store3, infoBook, configMenu;
    public GameObject lojaButton1, lojaButton2, changeToolButton1, changeToolButton2;
    public Sprite button1, button2, secunbutton1, secunbutton2;
    public GameObject textoDisplay;

    public GameObject casa1, casa2;
    public GameObject infoRodada1, infoRodada2, infoRodada3;

    WaveScore waveScore;

    void Start()
    {
        waveScore = FindObjectOfType<WaveScore>();
    }

    private void Awake()
    {
        uiMenus = this;
    }
    
    private void Update()
    {
        if (waveScore.allTurnosCount == 7)
        {
            casa1.SetActive(true);
        }

        if (waveScore.allTurnosCount == 14)
        {
            casa2.SetActive(true);
        }
    }
    public void OpenNotes()
    {
        if (!infoBook.gameObject.activeInHierarchy) 
        {
            if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- abrindo gaveta livro");
            if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Livro abrindo");
            infoBook.SetActive(true);
        }
        else if (infoBook.gameObject.activeInHierarchy) 
        {
            if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Fechando gaveta livro");
            if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Livro fechando");
            infoBook.SetActive(false);
        }
        mainStore.SetActive(false);
        mainStore2.SetActive(false);
        store1.SetActive(false);
        store2.SetActive(false);
        store3.SetActive(false);
        configMenu.SetActive(false);
        
        infoRodada1.SetActive(false);
        infoRodada2.SetActive(false);
        infoRodada3.SetActive(false);

    }
    public void CloseNotes()
    {
        if (infoBook.gameObject.activeInHierarchy) infoBook.SetActive(false);
    }
    public void OpenConfigMenu()
    {
        if (!configMenu.gameObject.activeInHierarchy) configMenu.SetActive(true);
        else if (configMenu.gameObject.activeInHierarchy) configMenu.SetActive(false);

        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");
    }
    public void CloseConfigMenu()
    {
        if (configMenu.gameObject.activeInHierarchy) configMenu.SetActive(false);
    }

    public void OpenMainStore()
    {
        if (!mainStore.activeInHierarchy)
        {
            mainStore.SetActive(true);
            changeToolButton1.SetActive(false);
            changeToolButton2.SetActive(false);
            lojaButton1.GetComponent<Image>().sprite = button2;          
        }
        else if (mainStore.activeInHierarchy)
        {
            CloseLojas();
            changeToolButton1.SetActive(true);
            changeToolButton2.SetActive(false);
        }

        infoRodada1.SetActive(false);
        infoRodada2.SetActive(false);
        infoRodada3.SetActive(false);

        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão Loja");
    }

    public void CloseLojas()
    {
        mainStore.SetActive(false);
        mainStore2.SetActive(false);
        store2.SetActive(false);
        store3.SetActive(false);
        store1.SetActive(false);
        changeToolButton1.SetActive(true);
        lojaButton1.GetComponent<Image>().sprite = button1;
    }

    public void OpenMainStore2()
    {
        if (!mainStore2.activeSelf)
        {
            mainStore2.SetActive(true);
            changeToolButton2.SetActive(false);
            changeToolButton1.SetActive(false);
            lojaButton2.GetComponent<Image>().sprite = secunbutton2;
        }
        else if (mainStore2.activeSelf)
        {
            mainStore2.SetActive(false);
            changeToolButton1.SetActive(true);
            changeToolButton2.SetActive(false);
        }

        infoRodada1.SetActive(false);
        infoRodada2.SetActive(false);
        infoRodada3.SetActive(false);
        infoBook.SetActive(false);
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão Loja");
    }

    public void OpenStore1()
    {
        if (!store1.gameObject.activeInHierarchy && !store2.gameObject.activeInHierarchy && !store3.gameObject.activeInHierarchy)
        {
             store1.SetActive(true);
             //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");
        }
        else if (!store1.gameObject.activeInHierarchy && store2.gameObject.activeInHierarchy || store3.gameObject.activeInHierarchy)
        {
            store2.SetActive(false);
            store3.SetActive(false);
            store1.SetActive(true);
        }
        else if (store1.gameObject.activeInHierarchy) store1.SetActive(false);
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");

        
    }
    public void OpenStore2()
    {
        if (!store2.gameObject.activeInHierarchy && !store1.gameObject.activeInHierarchy && !store3.gameObject.activeInHierarchy) 
        {
            store2.SetActive(true);
            
        }
        else if (!store2.gameObject.activeInHierarchy && store1.gameObject.activeInHierarchy || store3.gameObject.activeInHierarchy)
        {
            store1.SetActive(false);
            store3.SetActive(false);
            store2.SetActive(true);
        }
        else if (store2.gameObject.activeInHierarchy) store2.SetActive(false);
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");
    }
    public void OpenStore3()
    {
        if (!store3.gameObject.activeInHierarchy && !store1.gameObject.activeInHierarchy && !store2.gameObject.activeInHierarchy) 
        {
            store3.SetActive(true);
            //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");
        }
        else if (!store3.gameObject.activeInHierarchy && store2.gameObject.activeInHierarchy || store1.gameObject.activeInHierarchy)
        {
            store2.SetActive(false);
            store1.SetActive(false);
            store3.SetActive(true);
        }
        else if (store3.gameObject.activeInHierarchy) store3.SetActive(false);
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");
    }

    public void SetLojaButtonImage(int botao)
    {
        if (botao == 1) lojaButton1.GetComponent<Image>().sprite = button1;
        else lojaButton1.GetComponent<Image>().sprite = button2;
        
    }

    public void ChangeToolButton()
    {
        if (lojaButton1.activeSelf)
        {
            lojaButton1.SetActive(false);
            lojaButton2.SetActive(true);
            mainStore.SetActive(false);
            store1.SetActive(false);
            store2.SetActive(false);
            store3.SetActive(false);

            
        }
        else if (lojaButton2.activeSelf)
        {
            lojaButton2.SetActive(false);
            lojaButton1.SetActive(true);
            mainStore2.SetActive(false);
        }

        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 2");
    }

    public void DisplayTextDestroyTool(bool active, string frase)
    {
        textoDisplay.SetActive(active);
        textoDisplay.transform.GetChild(0).GetComponent<Text>().text = frase;
    }

    public void Clickou()
    {
        if(waveScore.allTurnosCount >= 1  && waveScore.allTurnosCount <= 6 )
        {
            infoRodada1.SetActive(true);
        }
        else if(waveScore.allTurnosCount >= 7 && waveScore.allTurnosCount <= 13)
        {
            infoRodada2.SetActive(true);
        }
        else if(waveScore.allTurnosCount >= 14)
        {
            infoRodada3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");

        mainStore.SetActive(false);
        mainStore2.SetActive(false);
        store1.SetActive(false);
        store2.SetActive(false);
        store3.SetActive(false);
        infoBook.SetActive(false); 
        configMenu.SetActive(false);
        
    }

    public void FechaLivro()
    {
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Livro fechando");
    }

    public void PassaPagina()
    {
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- pagina virando");
    }

    public void ClickSound()
    {
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");
    }

    public void Turno()
    {
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão turno");
    }

    public void FechaTudo()
    {
        mainStore.SetActive(false);
        mainStore2.SetActive(false);
        store1.SetActive(false);
        store2.SetActive(false);
        store3.SetActive(false);
        infoBook.SetActive(false);
        //configMenu.SetActive(false);
        infoRodada1.SetActive(false);
        infoRodada2.SetActive(false);
        infoRodada3.SetActive(false);
        lojaButton1.GetComponent<Image>().sprite = button1;
        
    }

}
