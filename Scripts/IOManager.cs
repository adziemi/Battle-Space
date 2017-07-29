using UnityEngine;

public class IOManager : MonoBehaviour  {

    public Vector2 mouse;
    public Vector2 steer;
    public bool pushRight;
    public bool pushLeft;

    void Start () {
        mouse = new Vector2(0f,0f);
        steer = new Vector2(0f,0f);
        pushRight = false;
        pushLeft = false;
    }

    void Update () {
        
        steer.Set(Input.GetAxis("Horizontal") * 10.0f * Time.deltaTime, -Input.GetAxis("Vertical") * 10.0f * Time.deltaTime);
        mouse.Set(Input.GetAxis("Mouse X") * Time.deltaTime, Input.GetAxis("Mouse Y") * Time.deltaTime);
        pushRight = Input.GetKeyDown("e");
        pushLeft = Input.GetKeyDown("q");
    }
}
