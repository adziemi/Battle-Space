using UnityEngine;

public class Gun : MonoBehaviour {
    public GameObject bullet;
    public Transform FirePoint;
    public Transform target;
    bool shoot = true;
    float range = 50f;
    public float distanceToEnemy;
}
