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

        foreach(Touch touch in Input.touches)
        {

            Vector3 touchedPoint = myCamera.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit;

            hit = Physics2D.Raycast(touchedPoint, Vector2.zero, Mathf.Infinity, touchInputMask);

            if (hit)
            {
                GameObject recipient = hit.transform.gameObject;
                
                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("OnTouchMoved", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                //if (touch.phase == TouchPhase.Canceled)
                //{
                //    recipient.SendMessage("OnTouchCancel", hit.point, SendMessageOptions.DontRequireReceiver);
                //}

                //if (touch.phase == TouchPhase.Stationary)
                //{
                //    recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                //}
            }
        }
	
	}
}
