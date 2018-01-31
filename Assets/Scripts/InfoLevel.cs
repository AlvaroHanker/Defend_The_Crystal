using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoLevel : MonoBehaviour {

    public bool random;
    // Use this for initialization
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
 
}
