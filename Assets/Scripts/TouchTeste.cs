using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchTeste : MonoBehaviour
{
    public Camera cam;
    Vector2 touchWorldPos;
    public UnityEvent openMenuToolPreview, destroyObject;


    // Start is called before the first frame update
    void Awake()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchWorldPos = cam.ScreenToWorldPoint(touch.position);
                    print(touchWorldPos);
                    if (CheckTouch(touchWorldPos))
                    {
                        openMenuToolPreview.Invoke();
                    }
                    break;

                case TouchPhase.Ended:
                    
                    break;
            }
        }
    }

    private bool CheckTouch(Vector2 touchWorldPos)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(touchWorldPos, cam.transform.forward);
        if (hitInfo.collider != null && hitInfo.collider.CompareTag("Touch"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
