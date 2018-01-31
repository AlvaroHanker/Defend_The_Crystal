using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LifeEnemy : MonoBehaviour {

    // Use this for initialization

    public GameObject explosionPrefab;
    public GameObject punt;
    private GameObject nave;
    private bool getDam = false;
    public float life;
    private Material[] materiales;
    private Color blink;
    private Color black;
    private float returnToPrev;
    Quaternion qt;
    public Vector3 pos;

    public void Start() {
        nave = GameObject.FindGameObjectWithTag("Nave");
        materiales = this.gameObject.GetComponent<MeshRenderer>().materials;
        blink = new Color(10.0f, 10.0f, 10.0f, 10.0f);
        black = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        
    }

    void OnCollisionEnter(Collision col) {
        
        if (col.gameObject.tag == "Bullet") {
            life -= nave.GetComponent<Shoot>().damage;
            Destroy(col.gameObject);
            for (int i = 0; i < 3; i++) {
                Material mt = materiales[i];
                mt.SetColor("_EmissionColor", blink);
            }
            pos = col.gameObject.transform.position;
            pos.z -= 1.0f;
            
            qt = this.transform.rotation;
            qt.x = 0;
            qt.y = 0;
            
            returnToPrev = Time.time + 0.2f;
            var explosion = (GameObject)Instantiate(
            explosionPrefab,
            pos,
            qt);
            explosion.transform.parent = this.gameObject.transform;           
            
        }
        if (col.gameObject.name == "EsferaDeMuerte") 
            Destroy(this.gameObject);
        }
	
	// Update is called once per frame
	void Update () {
        if (life <= 0) {
            ExecuteEvents.Execute<Iscore>(punt, null, (x, y) => x.AddScore(10));
            Destroy(this.gameObject);
        }
            
        if (Time.time > returnToPrev) {
            for (int i = 0; i < 3; i++) {
                Material mt = materiales[i];
                mt.SetColor("_EmissionColor", black);
            }
        }
    }
}
