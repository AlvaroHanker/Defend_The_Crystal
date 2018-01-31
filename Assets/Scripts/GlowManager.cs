using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowManager : MonoBehaviour {

    private Renderer rend;
    public Color init;
    public Color fin;
    private Color lerp;
    private Color blink;
    private bool entra = true;
    private float returnToPrev;
	// Use this for initialization
	void Start () {
        rend = this.gameObject.GetComponent<Renderer>();
        init = new Color(1.5f, 1.5f, 1.5f, 1.0f);
        fin = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        blink = new Color(10.0f, 10.0f, 10.0f, 10.0f);

	}

    void OnCollisionEnter(Collision col) {
        //8 es la capa del fuego enemigo
        if (col.gameObject.layer == 8) {
            Debug.Log(col.gameObject.name);
            entra = false;
            returnToPrev = Time.time + 0.2f;
            rend.material.SetColor("_EmissionColor", blink);
            Destroy(col.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (entra) {
            lerp = Color.Lerp(init, fin, Mathf.PingPong(Time.time * 0.5f, 1));
            rend.material.SetColor("_EmissionColor", lerp);
        }
        else if (Time.time > returnToPrev) {
            entra = true;
        }
        
    }
}
