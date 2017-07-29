using UnityEngine;

public class PlaneteMovement : MonoBehaviour {
    public float timeCounter = 3;
    public float speed = 0.001f;
    public float width = 18000;
    public float height = 18000;
    public float deep = -18000;
    public float initial_x = 15000;
    public float initial_y = 0;
    public float initial_z = 0;

    void Start () {
        
	}

    void Update () {
        timeCounter += Time.deltaTime * speed;
        float x = initial_x + Mathf.Cos(timeCounter) * width;
        float y = initial_y + Mathf.Sin(timeCounter) * deep;
        float z = initial_z + Mathf.Sin(timeCounter) * height;

        gameObject.transform.position = new Vector3(x, y, z);
        gameObject.transform.Rotate(new Vector3(0f, 1.0f*Time.deltaTime, 0f));
	}
}
