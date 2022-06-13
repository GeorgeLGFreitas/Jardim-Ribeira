using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //FMOD.Studio.Bus MasterBus;
    public static bool gamePaused;
    public bool isCutscene;
    public static bool mute;

    private void Awake()
    {
        gamePaused = false;
        mute = false;
    }

    private void Start()
    {
        //MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
        if(isCutscene)
        {
            Invoke("PlayGame", 13.5f);
        }
    }
    public void PlayGame()      //equivalente a NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Fechouu");
        Application.Quit();
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        //PlayerManager.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gamePaused = false;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        //PlayerManager.pause = false;
        SceneManager.LoadScene(1);
        //MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        gamePaused = false;
    }

    

    public void LoadLevel(int numberLevel)
    {
        SceneManager.LoadScene(numberLevel);
    }

    public void DeactivateObj() { this.gameObject.SetActive(false); }
    public void PlaySound(string path) { FMODUnity.RuntimeManager.PlayOneShot(path); }

    public void SomClick()
    {
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Botão UI - op 1");
    }
}
