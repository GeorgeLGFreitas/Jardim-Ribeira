using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Camera myCamera;
    private float width, height;
    private Vector3 startTouchPosition, endTouchPosition;
    private bool isPinching, canPan, canZoomIn, canZoomOut, canMove;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    public float minZoom, maxZoom;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;
    [SerializeField]
    float zoomModifierSpeed = 0.1f;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();
        isPinching = false;
        canPan = true;
        canZoomIn = canZoomOut = canMove = true;

        height = 2f * myCamera.orthographicSize;
        width = height * myCamera.aspect;
        //The camera's height is twice its orthographic size; you can find its width by multiplying its height by its aspect:
    }

    private void Update()
    {
        height = 2f * myCamera.orthographicSize;
        width = height * myCamera.aspect;

        if (myCamera.orthographicSize < minZoom) canZoomOut = true;
        else canZoomOut = false;

        if (myCamera.orthographicSize > maxZoom) canZoomIn = true;
        else canZoomIn = false;

        if(canMove) PanPintchCamera();
    }

    private void LateUpdate()
    {
        //se por algum acaso o ortographic size aumentar ou diminuir demais da conta, reseta aos seus valores mínimos
        if (myCamera.orthographicSize >= minZoom) myCamera.orthographicSize = minZoom;
        if (myCamera.orthographicSize <= maxZoom) myCamera.orthographicSize = maxZoom;

        CameraBounds();
    }

    private void CameraBounds()
    {
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, -50f + width/2f , 300f - width/2f),
                                  Mathf.Clamp(this.transform.position.y, -50f + height/2f, 140f - height/2f), -10f);
    }

    private void PanPintchCamera()
    {
        if (Input.touchCount == 1 && !isPinching)      //move camera
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = myCamera.ScreenToWorldPoint(touch.position);
                    //print(startTouchPosition.x);
                    break;

                case TouchPhase.Moved:
                    endTouchPosition = myCamera.ScreenToWorldPoint(touch.position);
                    if (CalculeLengthX() > 0.1 && CalculeLengthY() > 0.1) { canPan = true; } //sensibilidade

                    if (canPan && !isPinching)
                    {
                        myCamera.transform.position += (startTouchPosition - endTouchPosition);
                        myCamera.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, -10f);
                    }
                    //print(endTouchPosition.x);
                    break;

                case TouchPhase.Ended:
                    canPan = false;
                    break;


            }

        }
        else if (Input.touchCount == 2)     //pinch camera
        {
            isPinching = true;
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (firstTouch.phase == TouchPhase.Moved && secondTouch.phase == TouchPhase.Moved)
            {
                if (touchesPrevPosDifference > touchesCurPosDifference && canZoomOut)
                    myCamera.orthographicSize += zoomModifier;
                if (touchesPrevPosDifference < touchesCurPosDifference && canZoomIn)
                    myCamera.orthographicSize -= zoomModifier;
            }
        }
        else if(Input.touchCount <= 0)
        {
            isPinching = false;
        }

    }

    public void SetCanMoveCamera(bool canMove){ this.canMove = canMove; }
    public bool GetCanMoveCamera() { return this.canMove; }
    private float CalculeLengthX(){ return Mathf.Abs(endTouchPosition.x - startTouchPosition.x); }
    private float CalculeLengthY() { return Mathf.Abs(endTouchPosition.y - startTouchPosition.y); }
}
