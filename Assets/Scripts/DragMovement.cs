using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;

public class DragMovement : MonoBehaviour
{
    Vector3 posIni, posFin;
    float tempo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //print("toques: " + Input.touchCount);
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch toque = Input.GetTouch(i);

                print("toque: " + i + "posição " + toque.position);
                print("convertido: " + Camera.main.ScreenToWorldPoint(toque.position));

                if (toque.phase == TouchPhase.Began)
                {
                    posIni = Camera.main.ScreenToWorldPoint(toque.position);
                    tempo = 0;
                }
                else if (toque.phase == TouchPhase.Ended)
                {
                    posFin = Camera.main.ScreenToWorldPoint(toque.position);
                    print("movimento: " + (posFin - posIni));

                    if (tempo > 1)
                    {
                        GetComponent<Rigidbody2D>().velocity = (posFin - posIni).normalized * 5;
                    }

                }

            }


        }
        tempo += Time.deltaTime;

    }
}
