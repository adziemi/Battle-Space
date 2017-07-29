using UnityEngine;

public class FauxGravityBody : MonoBehaviour {
    public FauxGravityAttractor attractor;
    private Transform myTrasform;
    private Rigidbody rigidBody;

    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.useGravity = false;
        myTrasform = transform;
	}
	
	void Update () {
        attractor.Attract(myTrasform);
	}
}
