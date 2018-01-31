using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    public GameObject leftShip;
    public GameObject rightShip;
    private Vector2 touchDeltaPosition;
    private Vector2 globalDeltaPosition;
    private Vector2 ray2d;
    private Vector3 target;
    private Vector3 origin;
    private Vector3 p;
    private Vector3 paux;
    private RaycastHit2D rayhit;
    private RaycastHit rayhit3d;
    private Ray ray;



    void Start() {
       
    }

    void FixedUpdate() {

        if (Input.touchCount >0 && Input.touchCount <= 3) {

            foreach (Touch touchAux in Input.touches) {                
                ray = Camera.main.ScreenPointToRay(touchAux.position);
                p = Camera.main.ScreenToWorldPoint(touchAux.position);
                paux = p;
                p.z = 0;
                Debug.DrawLine(Vector3.zero, p, Color.white, 3.0f);
                ray2d = new Vector2(p.x, p.y);
                rayhit = Physics2D.Raycast(ray2d, Vector2.zero);

                if (rayhit.collider != null) {

                    if (rayhit.collider.gameObject.name == "RightTouch") {
                        rightShip.GetComponent<DigitRotateV2>().touchID = touchAux.fingerId;
                        rightShip.GetComponent<DigitRotateV2>().p = p;
                        rightShip.GetComponent<DigitRotateV2>().state = 1;
                    }
                    else if (rayhit.collider.gameObject.name == "LeftTouch") {
                        leftShip.GetComponent<DigitRotateV2>().touchID = touchAux.fingerId;
                        leftShip.GetComponent<DigitRotateV2>().p = p;
                        leftShip.GetComponent<DigitRotateV2>().state = 1;
                    }
                    else {
                        rightShip.GetComponent<DigitRotateV2>().state = 0;
                        leftShip.GetComponent<DigitRotateV2>().state = 0;
                    }
                }
                else {
                    rightShip.GetComponent<DigitRotateV2>().state = 0;
                    leftShip.GetComponent<DigitRotateV2>().state = 0;
                }
            }
        }
       


    }

    // Update is called once per frame
    void Update () {
		
	}
}
