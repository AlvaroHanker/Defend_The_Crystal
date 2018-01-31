using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour {

    public GameObject obj1;
    public GameObject obj2;
    public Touch touch1;
    public Touch touch2;
    private int t1 = 0;
    private int t2 = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1) {
            touch1 = Input.GetTouch(Input.touches.Length-1);
            t1 = 1;

        }
            
        if (Input.touchCount == 2) {
            touch2 = Input.GetTouch(Input.touches.Length - 1);
            t2 = 1;
        }
            
        if (Input.touchCount == 1 && t1 == 1) 
            Debug.Log("touch 1: " + touch1.fingerId.ToString());

        if (Input.touchCount == 2 && t2 == 1)
            Debug.Log("touch 2: " + touch2.fingerId.ToString());

        Debug.Log("Valores de t, [t1,t2]: " + t1.ToString() + "," + t2.ToString());
        //Debug.Log(Quaternion.Angle(obj1.transform.rotation, obj2.transform.rotation).ToString());
    }

    void LateUpdate() {
        if (touch1.phase == TouchPhase.Ended) 
            t1 = 0;
        if (touch2.phase == TouchPhase.Ended)
            t2 = 0;


    }
    }
