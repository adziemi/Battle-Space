using UnityEngine;

public class RockAcceleration : MonoBehaviour {
    private Rigidbody rigidBody;
    public Transform target;
    public float force = 20.0f;

    void Start () {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(dir * force, ForceMode.Impulse);
    }
}
