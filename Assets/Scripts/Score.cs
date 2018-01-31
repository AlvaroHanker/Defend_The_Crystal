using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public interface Iscore : IEventSystemHandler {

    void AddScore(int score);
    void SubScore(int score);
}

public class Score : MonoBehaviour, Iscore {

    private int punt;

     public void AddScore(int score) {
        string texto = this.gameObject.GetComponent<Text>().text;
        Debug.Log(texto);
        int.TryParse(texto, out punt);
        punt += score;
        this.gameObject.GetComponent<Text>().text = PutZeros(punt.ToString());

    }
    public void SubScore(int score) {


    }

    private string PutZeros(string init) {

        string ret = "";
        for (int i = 0; i < (6- init.Length); i++)
            ret += "0";
        ret += init;
        return ret;
    }
}
