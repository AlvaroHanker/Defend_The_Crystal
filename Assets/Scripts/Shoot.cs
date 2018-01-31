using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform shootSpawn;
    public float fireRate;
    private float nextFire;
    private int state;
    public float damage;
    
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        state = this.gameObject.GetComponent<DigitRotateV2>().state;
        if (Input.touchCount >= 1 && Input.GetTouch(0).phase != TouchPhase.Ended && Time.time > nextFire && state !=0) {
            nextFire = Time.time + fireRate;
            fire();
        }
        
	}

    void fire() {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            shootSpawn.position,
            shootSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 12;
        Destroy(bullet, 1.8f);
    }
}
