using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInputScript : MonoBehaviour {

    public LayerMask touchInputMask;
    private Camera myCamera;

    // Use this for initialization
    void Start () {

        myCamera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        bool handled = false;

        foreach (Touch touch in Input.touches)
        {

            handled = true;

            Vector3 touchedPoint = myCamera.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchedPoint, Vector2.zero, Mathf.Infinity, touchInputMask);

            if (hit)
            {
                GameObject recipient = hit.transform.gameObject;
                HandleTouch(recipient, hit.point, touch.phase);
            }
        }

        //for testing on a desktop with mouse
        if (Input.GetMouseButtonDown(0) && !handled)
        {

            Vector3 clickedPoint = myCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickedPoint, Vector2.zero, Mathf.Infinity, touchInputMask);

            if (hit)
            {
                GameObject recipient = hit.transform.gameObject;
                HandleTouch(recipient, hit.point,TouchPhase.Began);
            }

        }

    }

    void HandleTouch(GameObject recipient, Vector2 touchPoint, TouchPhase tp)
    {

        switch (tp)
        {
            case TouchPhase.Began:
                recipient.SendMessage("OnTouchDown", touchPoint, SendMessageOptions.DontRequireReceiver);
                break;
            case TouchPhase.Moved:
                recipient.SendMessage("OnTouchMoved", touchPoint, SendMessageOptions.DontRequireReceiver);
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Ended:
                recipient.SendMessage("OnTouchUp", touchPoint, SendMessageOptions.DontRequireReceiver);
                break;
            case TouchPhase.Canceled:
                break;
        }   

    }
}
