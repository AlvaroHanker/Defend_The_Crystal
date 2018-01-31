using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TupleManager : MonoBehaviour {


    public List<string> tuplesStr = new List<string>();
    public List<EnemyData> infoFase = new List<EnemyData>();
    public List<List<EnemyData>> fases = new List<List<EnemyData>>();
    public TextAsset jsonTuple;
    private string patternForPhases = "(\\([^;]*)";
    private string patternIntoPhases = "(\\([^,;]*)";
    private string patternForEnemies = "([0-9]*(?:\\.[0-9]*)?)";
    // Use this for initialization
    void Start () {
        string content = jsonTuple.text;
        tuplesStr = RegExpAnalizer.MapStrings(content, patternForPhases);
        foreach(string str in tuplesStr) {
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
        int i = 0;
        foreach(List<EnemyData> l in fases) {
            Debug.Log("de la fase" + i++ + " sus datos son:");
            foreach (EnemyData e in l)
                Debug.Log("su spawn es: " + e.GetSpawn().ToString());
            
        }
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private EnemyData ParseInfo(List<string> enemyInfo) {
        EnemyData nuevoEnemigo = new EnemyData(0.0f, 0, 0, 0.0f, 0.0f, 0);
        int i = 0;
        foreach(string str in enemyInfo) {
            float auxf;
            int auxi;
            if (i == 0 || i == 3 || i == 4) {
                auxf = float.Parse(str);
                nuevoEnemigo.setFloatValue(i, auxf);
            }
                
            else if (i == 1 || i == 2 || i == 5) {
                auxi = int.Parse(str);
                nuevoEnemigo.setIntValue(i,auxi);
            }
                
            i++;
        }

        return nuevoEnemigo;
    }


}
