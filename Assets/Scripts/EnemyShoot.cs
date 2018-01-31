using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform shootSpawn;
    private float nextFire;
    public float damage;
    public float fireRatio;
    private int state = -1;
    // Use this for initialization
    void Start () {
        nextFire = Time.time + Random.Range(0.0f,3.0f)*fireRatio;
	}

    void OnCollisionEnter(Collision col) {
        Debug.Log("hemos entrado" + col.gameObject.name);
        if (col.gameObject.name == "ColliderExterior") {

            state = 0;
        }
    }
    // Update is called once per frame
    void Update () {
		if (Time.time > nextFire) {
            nextFire = Time.time + Random.Range(3.0f, 5.0f) * fireRatio;
            fire();
        }
	}

    void fire() {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            shootSpawn.position,
            shootSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 12;
        Destroy(bullet, 5.0f);
    }
}
