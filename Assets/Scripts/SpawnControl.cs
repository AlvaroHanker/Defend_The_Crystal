using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnControl : MonoBehaviour {

    //variables para el manejo del spawn
    public List<Transform> spawns;
    public GameObject enemy;
    public GameObject punt;
    public GameObject changeStage;
    public GameObject finisLevel;
    public List<GameObject> enemies;
    public bool random;
    private int randNum;
    private float nextFire = -1.0f;
    
    
    //variables para manejar la información de las fases
    public TextAsset jsonTuple;
    private List<EnemyData> infoFase = new List<EnemyData>();
    private List<List<EnemyData>> fases = new List<List<EnemyData>>();
    private EnemyData actualEnemy;
    private string patternForPhases = "(\\([^;]*)";
    private string patternIntoPhases = "(\\([^,;]*)";
    private string patternForEnemies = "([0-9]*(?:\\.[0-9]*)?)";
    int i = 0;
    int j = 0;


    // Use this for initialization
    void Start() {
        Ignora_Colisiones();
        GetFases();
        if (GameObject.Find("Random") != null)
            random = true;
        else if (GameObject.Find("NoRandom") != null)
            random = false;

    }

    // Update is called once per frame
    void Update() {

        if (random)
            RandSpawn();
        else if (i<fases.Count){            
            infoFase = fases[i];
            if (Time.time >= nextFire) {
                if (j < infoFase.Count) {
                    actualEnemy = infoFase[j++];
                    var navenemy = (GameObject)Instantiate(
                    enemies[actualEnemy.GetEnemy()],
                    spawns[actualEnemy.GetSpawn()].position,
                    this.transform.rotation);

                    float vida = navenemy.GetComponent<LifeEnemy>().life;
                    float minimun = vida - (vida * actualEnemy.GetLive());
                    float maximun = vida + (vida * actualEnemy.GetLive());

                    navenemy.GetComponent<LifeEnemy>().life = (float)Random.Range(minimun, maximun);
                    navenemy.GetComponent<EnemyShoot>().fireRatio = actualEnemy.GetFireRatio();
                    
                    nextFire = Time.time + actualEnemy.GetTime();
                }
                else if (!GameObject.Find("enemy_simple(Clone)")){
                    //Aquí cambio de fase
                    Debug.Log("Cambio de fase");
                    changeStage.SetActive(true);
                    StartCoroutine(FinFase());
                    nextFire = Time.time + 7.0f;
                    i++;
                    j = 0;
                }
            }

        }
        else {
            Debug.Log("hemos entrado donde no debíamos");
            //Fin del nivel
            finisLevel.SetActive(true);
            StartCoroutine(FinLevel());
        }
    }

    private void Ignora_Colisiones() {
        Physics.IgnoreLayerCollision(8, 9, true);
        Physics.IgnoreLayerCollision(8, 11, true);
        Physics.IgnoreLayerCollision(8, 10, true);
        Physics.IgnoreLayerCollision(8, 13, true);
        Physics.IgnoreLayerCollision(9, 11, true);
        Physics.IgnoreLayerCollision(9, 13, true);
        Physics.IgnoreLayerCollision(9, 14, true);
        Physics.IgnoreLayerCollision(10, 11, true);
        Physics.IgnoreLayerCollision(10, 13, true);
        Physics.IgnoreLayerCollision(11, 11, true);
        Physics.IgnoreLayerCollision(12, 11, true);
        Physics.IgnoreLayerCollision(12, 10, true);
        Physics.IgnoreLayerCollision(12, 9, true);
        Physics.IgnoreLayerCollision(12, 8, true);
    }

    private void RandSpawn (){

        if (Time.time > nextFire) {

            randNum = (int)Random.Range(0, 22);
            var navenemy = (GameObject)Instantiate(
                enemy,
                spawns[randNum].position,
                this.transform.rotation);
            navenemy.GetComponent<LifeEnemy>().punt = punt;
            nextFire = Time.time + 2f;
        }
    }

    private void GetFases() {

        List<string> tuplesStr = new List<string>();
        string content = jsonTuple.text;
        tuplesStr = RegExpAnalizer.MapStrings(content, patternForPhases);
        foreach (string str in tuplesStr) {
            List<string> intoPhases = new List<string>();
            intoPhases = RegExpAnalizer.MapStrings(str, patternIntoPhases);
            infoFase = new List<EnemyData>();
            foreach (string str2 in intoPhases) {
                List<string> enemyInfo = new List<string>();
                enemyInfo = RegExpAnalizer.MapStrings(str2, patternForEnemies);
                EnemyData enem = ParseInfo(enemyInfo);
                infoFase.Add(enem);
            }
            fases.Add(infoFase);
        }
    }
    private EnemyData ParseInfo(List<string> enemyInfo) {
        EnemyData nuevoEnemigo = new EnemyData(0.0f, 0, 0, 0.0f, 0.0f, 0);
        int i = 0;
        foreach (string str in enemyInfo) {
            float auxf;
            int auxi;
            if (i == 0 || i == 3 || i == 4) {
                auxf = float.Parse(str);
                nuevoEnemigo.setFloatValue(i, auxf);
            }

            else if (i == 1 || i == 2 || i == 5) {
                auxi = int.Parse(str);
                nuevoEnemigo.setIntValue(i, auxi);
            }

            i++;
        }

        return nuevoEnemigo;
    }
    private void CreateEnemy(EnemyData actualEnemy) {


    }

    IEnumerator FinFase() {
        yield return new WaitForSeconds(6);
        changeStage.SetActive(false);
    }

    IEnumerator FinLevel() {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Start");
    }
}
