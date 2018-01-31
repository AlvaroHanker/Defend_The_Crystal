using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LifeCristal : MonoBehaviour {

    public Slider sli;
    public float vidaCristal;
    private int vidas = 4;
    public GameObject vida1;
    public GameObject vida2;
    public GameObject vida3;
    public GameObject punt;
    // Use this for initialization
    void Start () {
		
	}
	
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.layer == 8) {
            sli.value -= 1;
            vidaCristal -= 1;
        }
    }
	// Update is called once per frame
	void Update () {

        if (vidaCristal <= 0) {
            //Mandar mensaje a uno de los cristales pequeños (o a los tres) para que explote uno          
            vidas-=1;
            if (vidas <= 0) {
                ExecuteEvents.Execute<Iexplosion>(punt, null, (x, y) => x.GameOver());

                //Game Over
            }
            else {
                if (vidas == 3)
                    ExecuteEvents.Execute<Iexplosion>(punt, null, (x, y) => x.ExplosionVida(vida1));
                else if (vidas ==2)
                    ExecuteEvents.Execute<Iexplosion>(punt, null, (x, y) => x.ExplosionVida(vida2));
                else if (vidas == 1)
                    ExecuteEvents.Execute<Iexplosion>(punt, null, (x, y) => x.ExplosionVida(vida3));
                sli.value = 100.0f;
                vidaCristal = 100.0f; 
            }
            
          }

		
	}
}
