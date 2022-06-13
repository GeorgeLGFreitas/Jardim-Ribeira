using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSimple : MonoBehaviour
{
    public CameraScript myCam;
    private Animator anim;
    public Text texto;
    public GameObject textoContinue, dialogueBox, nextDialogue, blockNextRoundButton;
    public float timeToSkip;
    public string[] textos;
    private float timer;
    private int i;
    private bool ended;


    private void Start()
    {
        anim = GetComponent<Animator>();
        i = 0;
        ended = false;

        
    }
    private void Awake()
    {
        
        texto.text = textos[i];
        StartCoroutine(TypeSentence(texto.text));
    }

    private void OnEnable()
    {
        i = 0;
        ended = false;
        myCam.SetCanMoveCamera(false);
        blockNextRoundButton.SetActive(true);
        if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Chars entrando em cena");
    }

    void Update()
    {
        if(timer < timeToSkip)                                    //o timer continua somando +1 a cada segundo e o "textoContinue"
        {   
            timer += 1 * Time.deltaTime;
            textoContinue.SetActive(false); 
        }
        else if (timer >= timeToSkip) { textoContinue.SetActive(true); } //se não o time for maior ou igual ao valor de skip, aparece o "textoContinue"

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && timer >= timeToSkip && !ended)
            {
                if (PlayerPrefs.GetInt("mute", 1) == 1) FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Passar dialogo");
                if (i >= textos.Length - 1)
                {
                    anim.SetTrigger("exit");
                    texto.gameObject.SetActive(true);
                    ended = true;

                    if (nextDialogue != null) { nextDialogue.SetActive(true); }

                    myCam.SetCanMoveCamera(true);
                    blockNextRoundButton.SetActive(false);
                }
                else
                {
                    texto.gameObject.SetActive(false);
                    i += 1;
                    texto.gameObject.SetActive(true);
                    texto.text = textos[i];
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(texto.text));
                    timer = 0;
                }
            }

        }
    }

    IEnumerator TypeSentence(string fala)
    {
        texto.text = "";
        foreach (char letter in fala.ToCharArray())
        {
            texto.text += letter;
            yield return null;
        }
    }


    public void TurnOn(){dialogueBox.SetActive(true);}
    public void DeactiveThisObject() { this.gameObject.SetActive(false); }

    public void SetCanMove(int booleana)
    {
        if (booleana == 0) myCam.SetCanMoveCamera(false);
        else if (booleana == 1) myCam.SetCanMoveCamera(true);
    }
}

