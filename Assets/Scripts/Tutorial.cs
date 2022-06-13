using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public CameraScript myCam;
    public GameObject[] objects;
    public GameObject nextDialogue, blockNextRoundButton;
    private float timer, timeToSkip;
    private int i;
    private bool ended;

    private void Awake()
    {
        timer = 0;
        timeToSkip = 2.5f;
        i = 0;
        ended = false;
    }

    private void Start()
    {
        objects[i].SetActive(true);
        myCam.SetCanMoveCamera(false);
        blockNextRoundButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && timer >= timeToSkip && !ended)
            {
                if(i >= objects.Length - 1)
                {
                    objects[i].SetActive(false);
                    this.gameObject.SetActive(false);
                    ended = true;

                    if (nextDialogue != null) { nextDialogue.SetActive(true); }
                    myCam.SetCanMoveCamera(true);
                    blockNextRoundButton.SetActive(false);
                }
                else
                {
                    i += 1;
                    if (i >= 1) objects[i - 1].SetActive(false);
                    objects[i].SetActive(true);
                    timer = 0;
                }
            }
        }
    }
}
