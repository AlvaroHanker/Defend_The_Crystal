using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotate : MonoBehaviour {

    public GameObject pivot;
    public float speed;
    public bool x;
    public bool y;
    public bool z;
    public bool clockwise;
    private Vector3 pivotPosition;
    private Vector3 axisRotation = new Vector3(0,0,0);
    private float angle = 0.0f;
    private float sentido = 1.0f;
	// Use this for initialization
	void Start () {
        if (!clockwise)
            sentido = -1.0f;
        if (x)
            axisRotation.x = 1;
        if (y)
            axisRotation.y = 1;
        if (z)
            axisRotation.z = 1;
    }
	
	// Update is called once per frame
	void Update () {
        angle += 1;
        pivotPosition = pivot.transform.position;
        this.gameObject.transform.RotateAround(pivotPosition, axisRotation, speed*sentido);
	}
}
