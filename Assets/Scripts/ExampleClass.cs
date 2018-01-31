using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExampleClass : MonoBehaviour {
    public float speed = 0.1F;
    public GameObject pivot;
    public GameObject scriptText;
    public Camera mainCamera;
    private Vector2 touchDeltaPosition;
    private Vector2 globalDeltaPosition;
    private Vector2 ray2d;
    private Vector3 target;
    private Vector3 origin;
    private Vector3 p;
    private Vector3 paux;
    private int state;
    private int actualState;
    private float rotation;
    private float initPos;
    private float acceleration;
    private float angle;
    private RaycastHit2D rayhit;
    private Ray ray;
    private bool notUp;
    private bool notDown;


    void Start() {
        state = 0;
        actualState = 1;
        notUp = false;
        notDown = false;
        initPos = this.transform.localPosition.x;
    }

    void FixedUpdate() {

        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began|| Input.GetTouch(0).phase == TouchPhase.Stationary)) {
            state = 0;
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            p = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            paux = p;
            paux.z = 0.0f;
            Debug.DrawRay(Vector3.zero, paux, Color.white, 5.0f);
            ray2d = new Vector2(p.x, p.y);
            rayhit = Physics2D.Raycast(ray2d, Vector2.zero);

            if (rayhit.collider != null) {
                if (rayhit.collider.gameObject.name == "RightTouch")
                    state = 1;
                else if (rayhit.collider.gameObject.name == "LeftTouch")
                    state = -1;
                //scriptText.GetComponent<Text>().text = "state = " + state.ToString() + " with collider : " + rayhit.collider.gameObject.name;
            }

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

        if (Input.touchCount == 1 && mustChange()) {
            actualState = state;
            if (state == -1) {
                this.gameObject.transform.localRotation = new Quaternion(this.gameObject.transform.localRotation.x, this.gameObject.transform.localRotation.y, 0f, this.gameObject.transform.localRotation.w);
                this.gameObject.transform.localPosition = new Vector3(-initPos, 0.0f, this.gameObject.transform.position.z);
            }

            else {
                this.gameObject.transform.localRotation = new Quaternion(this.gameObject.transform.localRotation.x, this.gameObject.transform.localRotation.y, -180f, this.gameObject.transform.localRotation.w);
                this.gameObject.transform.localPosition = new Vector3(initPos, 0.0f, this.gameObject.transform.position.z);
            }

        }
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) {
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

            //scriptText.GetComponent<Text>().text = this.gameObject.transform.rotation.ToString();

        }
    }

    private Vector3 getAngleFromRay() {

        return Vector3.zero;
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

    public int getState() {
        return state;
    }
}
