using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {
    private float gravity = -50f;
    public void Attract(Transform body) {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity *  1/Vector3.Distance(body.position,transform.position),ForceMode.VelocityChange);
    }
}
