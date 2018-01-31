using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitRotateV2 : MonoBehaviour {

    public float speed = 0.1F;
    public GameObject pivot;
    public GameObject digitCollider;
    public DigitRotateV2 elOtro;
    public Camera mainCamera;
    public int state;
    public int wise;
    public int index;
    public int touchID;
    public Vector3 p;
    private Vector2 touchDeltaPosition;
    private Vector2 globalDeltaPosition;
    private Vector2 ray2d;
    private Vector3 target;
    private Vector3 origin;
    
    private Vector3 paux;
    private int actualState;
    private float rotation;
    private float initPos;
    private float acceleration;
    private float angle;
    private RaycastHit2D rayhit;
    private RaycastHit rayhit3d;
    private Ray ray;
    private bool notUp;
    private bool notDown;
    private bool anim;
    private int layerMask;
    private Touch toque;
    public int touchState;




    void Start() {
        index = -1;
        layerMask = 1 << 12;
        state = 0;
        actualState = 1;
        notUp = false;
        notDown = false;
        initPos = this.transform.localPosition.x;
        touchState = -1;
    }

    void FixedUpdate() {

  
    }

    void OnCollisionEnter(Collision col) {

        if (col.gameObject.name == "UpperLimit")
            notUp = true;
        if (col.gameObject.name == "LowerLimit")
            notDown = true;
    }

    void OnCollisionExit(Collision col) {

        if (col.gameObject.name == "UpperLimit")
            notUp = false;
        if (col.gameObject.name == "LowerLimit")
            notDown = false;
    }

    void Update() {

        foreach (Touch touchAux in Input.touches) {
            if(touchAux.fingerId == touchID) {
                toque = touchAux;
            }
        }
        if (Input.touchCount >= 1 && state != 0) {
            if (Physics.Raycast(p, -p, out rayhit3d, 100, 1 << 12)) {
                Debug.Log("raycast bien :" + rayhit3d.collider.gameObject.name);
                this.gameObject.transform.position = rayhit3d.point;
                this.gameObject.transform.LookAt(p);

            }

        }

        /*if (Input.touchCount >= 1 && toque.phase == TouchPhase.Moved && state != 0) {
            // Get movement of the finger since last frame
            touchDeltaPosition = toque.deltaPosition;
            globalDeltaPosition = toque.position;
            // Get data for rotation movement
            origin = pivot.transform.position;
            //origin.y = 1.0f;
            target = mainCamera.ScreenToWorldPoint(new Vector3(globalDeltaPosition.x, globalDeltaPosition.y, 0.0f));
            target.z = 0.0f;
            rotation = Vector3.Angle(target, origin);
            acceleration = Mathf.Abs(touchDeltaPosition.y * speed) + 0.7f;
            if (canGoUp(touchDeltaPosition.y, notUp))
                this.gameObject.transform.RotateAround(origin, Vector3.forward, rotation * Mathf.Deg2Rad * acceleration * wise);

            else if (canGoDown(touchDeltaPosition.y, notDown))
                this.gameObject.transform.RotateAround(origin, Vector3.forward, -rotation * Mathf.Deg2Rad * acceleration * wise);
        }*/
    }

    void LateUpdate() {
        if (toque.phase == TouchPhase.Ended) {
            touchState = -1;
            state = 0;
        }
            
        
    }

    private bool canGoUp(float touch, bool up) {
        return touch > 0 && !up && state != 0;
    }

    private bool canGoDown(float touch, bool down) {
        return touch < 0 && !down && state != 0;
    }

    private bool mustChange() {
        return state != 0 && state != actualState;
    }

}
