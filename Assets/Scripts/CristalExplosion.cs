using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public interface Iexplosion : IEventSystemHandler {

    void ExplosionVida(GameObject Vida);
    void GameOver();

}

public class CristalExplosion : MonoBehaviour, Iexplosion {

    public GameObject explosionPrefab;
    public GameObject gameOver;
    public GameObject black;
    private Vector3 pos;
    private Quaternion qt;
    private bool entra = false;
    Animator animator;
	// Use this for initialization

    void Start() {
        animator = GetComponent<Animator>();
    }
    public void ExplosionVida(GameObject vida) {

        Destroy(vida);
        pos = this.transform.position;
        qt = this.transform.rotation;
        var explosion = (GameObject)Instantiate(
            explosionPrefab,
            pos, qt);
        Destroy(explosion, 2.3f);
        animator.SetTrigger("explosion");
    }

    public void GameOver() {
        gameOver.SetActive(true);
        black.SetActive(true);
        entra = true;
    }

    void Update() {
        if (entra){
            StartCoroutine(Change());

        }
    }

    IEnumerator Change() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Start");

    }
    
}
