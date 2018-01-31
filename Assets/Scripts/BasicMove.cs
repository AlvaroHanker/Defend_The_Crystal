using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour {

    public float speed;
    public Transform cristal;
    private Vector3 rot;
    private int state = -1;
	// Use this for initialization
	void Start () {
        cristal = GameObject.FindGameObjectWithTag("Cristal").GetComponent<Transform>();
        transform.LookAt(cristal);
        if ((transform.position.x-cristal.position.x) > 0) {
            rot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(rot.x, rot.y, 180f);
        }
             
    }
	
    void OnCollisionEnter(Collision col) {
        Debug.Log("hemos entrado" + col.gameObject.name);
        if (col.gameObject.name == "ColliderInterior") {          
            state = 1;
        }
        if (col.gameObject.name == "ColliderExterior") {
            this.gameObject.layer = 9;
            this.gameObject.GetComponent<EnemyShoot>().enabled = true;
        }
    }
	// Update is called once per frame
	void Update () {
        if (state > 0) {
            this.gameObject.GetComponent<EnemyShoot>().enabled = false;
            this.transform.Rotate(Vector3.right * Time.deltaTime * 100);
            speed += 1;
            this.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            Destroy(this.gameObject, 2f);
        }else {
            transform.LookAt(cristal);
            if ((transform.position.x - cristal.position.x) > 0) {
                rot = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(rot.x, rot.y, 180f);
            }
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
	}
}
