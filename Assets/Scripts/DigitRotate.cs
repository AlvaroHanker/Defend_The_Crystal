using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DigitRotate : MonoBehaviour {
    public float speed = 0.1F;
    public GameObject pivot;
    public GameObject rightCollider;
    public GameObject leftCollider;
    public Camera mainCamera;
    public int state;
    private Vector2 touchDeltaPosition;
    private Vector2 globalDeltaPosition;
    private Vector2 ray2d;
    private Vector3 target;
    private Vector3 origin;
    private Vector3 p;
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


    void Start() {
        layerMask = 1 << 12;
        state = 0;
        actualState = 1;
        notUp = false;
        notDown = false;
        initPos = this.transform.localPosition.x;
    }

    void FixedUpdate() {


        if (Input.touchCount == 1 && (Input.GetTouch(0).phase != TouchPhase.Ended)) {
            state = 0;
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            p = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            paux = p;
            p.z = 0;
            //Debug.DrawLine(Vector3.zero, p, Color.white, 3.0f);
            ray2d = new Vector2(p.x, p.y);
            rayhit = Physics2D.Raycast(ray2d, Vector2.zero);

            if (rayhit.collider != null) {
                if (rayhit.collider.gameObject.name == "RightTouch") {
                    state = 1;
                    leftCollider.SetActive(false);
                }
                   
                else if (rayhit.collider.gameObject.name == "LeftTouch") {
                    state = -1;
                    rightCollider.SetActive(false);
                }
                   
                else
                    state = 0;
            }
            else
                state = 0;
        }
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


        if (Input.touchCount == 2)
            state = 0;


        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && state != 0) {
            if (Physics.Raycast(p, -p, out rayhit3d, 100, 1 << 12)) {
                Debug.Log("raycast bien :" + rayhit3d.collider.gameObject.name);
                this.gameObject.transform.position = rayhit3d.point;
                this.gameObject.transform.LookAt(p);
            }
               
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && state != 0) {
            // Get movement of the finger since last frame
            touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            globalDeltaPosition = Input.GetTouch(0).position;
            // Get data for rotation movement
            origin = pivot.transform.position;
            //origin.y = 1.0f;
            target = mainCamera.ScreenToWorldPoint(new Vector3(globalDeltaPosition.x, globalDeltaPosition.y, 0.0f));
            target.z = 0.0f;
            rotation = Vector3.Angle(target, origin);
            acceleration = Mathf.Abs(touchDeltaPosition.y * speed) + 0.7f;
            if (canGoUp(touchDeltaPosition.y, notUp))
                this.gameObject.transform.RotateAround(origin, Vector3.forward, rotation * Mathf.Deg2Rad * acceleration * state);

            else if (canGoDown(touchDeltaPosition.y, notDown))
                this.gameObject.transform.RotateAround(origin, Vector3.forward, -rotation * Mathf.Deg2Rad * acceleration * state);

        }
    }

    void LateUpdate() {
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended)) {
            rightCollider.SetActive(true);
            leftCollider.SetActive(true);
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
