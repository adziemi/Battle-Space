using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private float health = 20f;
    private bool alive = true;
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) Death();
    }
    private void Death()
    {
        alive = false;   
    }
    public bool isDead() {
        if (!alive) return true;
        else return false;
    }
    public float getHealth() { return health; }
    
}
