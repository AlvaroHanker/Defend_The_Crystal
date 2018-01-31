using System.Collections;
using System.Collections.Generic;

public class EnemyData {

    /* plantilla de tupla de enemigo: (T E M FR V S)
     * 
     * donde:
     *       T(float)   : tiempo que ha de pasar hasta que aparezca el siguiente enemigo.
     *       E(integer) : Identificador del tipo de enemigo que aparece en esta parte.
     *       M(integer) : Identificador del tipo de movimiento que tendrá el enemigo.
     *       FR(float)  : Frecuencia de disparo del enemigo.
     *       V(float)   : Holgura máxima de vida sobre la vida base del enemigo que se le sumará/restará al mismo cuando se cree.
     *       S(integer) : Identificador del spawn del que saldrá el enemigo.
     *       
     * Ejemplo de Tupla: (1 0 0 1 0.4 18)*/
    private float tiempoNext;
    private int enemigo;
    private int movimiento;
    private float frecDisp;
    private float vida;
    private int spawn;

    public EnemyData(float t, int e, int m, float fr, float v, int s){
        this.tiempoNext = t;
        this.enemigo = e;
        this.movimiento = m;
        this.frecDisp = fr;
        this.vida = v;
        this.spawn = s;
    }   
    
    public float GetTime(){
        return tiempoNext;
    }
    public int GetEnemy(){
        return enemigo;
    }
    public int GetMovement(){
        return movimiento;
    }
    public float GetFireRatio(){
        return frecDisp;
    }
    public float GetLive(){
        return vida;
    }
    public int GetSpawn(){
        return spawn;
    }
    public void setFloatValue(int i, float val) {
        if (i == 0)
            this.tiempoNext = val;
        else if (i == 3)
            this.frecDisp = val;
        else if (i == 4)
            this.vida = val;
        else
            return;
    }
    public void setIntValue(int i, int val) {
        if (i == 1)
            this.enemigo = val;
        else if (i == 2)
            this.movimiento = val;
        else if (i == 5)
            this.spawn = val;
        else
            return;

    }
}
