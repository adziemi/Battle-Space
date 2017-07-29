using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDust : MonoBehaviour {

    private Transform t;
    private ParticleSystem.Particle[] points;
    private ParticleSystem particleSystem;
    public int dustMaxAmount = 100;
    public float dustSize = 1.0f;
    public float dustDistance = 1;
    private float dustDistanceSqr;
    [Range(0, 1)] public float dustOpacity;

    void Start () {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        t = transform;
        dustDistanceSqr = dustDistance * dustDistance;
	}

    private void Createdust() {
        points = new ParticleSystem.Particle[dustMaxAmount];
        for (int i = 0; i < dustMaxAmount; i++) {
            points[i].position = Random.insideUnitSphere * dustDistance + t.position;
            points[i].startColor = new Color(1,1,1,dustOpacity);
            points[i].startSize = dustSize;
        }
    }

    void Update () {
        if (points == null) Createdust();

        for (int i = 0; i < dustMaxAmount; i++) {
            if ((points[i].position - t.position).sqrMagnitude > dustDistanceSqr) {
                points[i].position = Random.insideUnitSphere * dustDistance + t.position;
            }
        }

        particleSystem.SetParticles(points, points.Length);
	}
}
