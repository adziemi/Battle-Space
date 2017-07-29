using UnityEngine;
using UnityEngine.UI;

public class PlayerShipController : MonoBehaviour
{
    [Header("Unity Objects")]
    public GameObject Bullet;
    public Transform SpaceShip;
    public Transform FirePoint1;
    public Transform FirePoint2;
    private int lastFirePoint = 1;
    public ParticleSystem flame1;
    public ParticleSystem flame2;
    public ParticleSystem flame3;
    public ParticleSystem flame4;
    
    private Rigidbody rigidBody;
    
    private Target target;
    private Vector3 lastPosition = Vector3.zero;
    private float lastSpeed;
    private bool enginesEnabled = true;

    public CameraController cameraController;
    public IOManager io;
    
    [Header("Parameters")]
    public float fuelAmount = 100;
    private float fuelMaxAmount = 100;
    public float fuelType = 1;
    public float fuelRefeneration = 0.2f;
    public float fuelCombustion = 2.0f;
    private float maxSpeed = 20f;
    private float normalDrag = 0.5f;
    private float minDrag = 0.0f;
    private float pushStrength = 100f;
    float orientationChangeSensitivity = 1f;


    [Header("UI components")]
    public Text fuelInfo;
    public Text HPInfo;
    public Text SpeedInfo;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidBody = gameObject.GetComponent<Rigidbody>();
        target = gameObject.GetComponent<Target>();
        flame1.Stop();
        flame2.Stop();
        flame3.Stop();
        flame4.Stop();
    }

    void FixedUpdate()
    {

        lastPosition = transform.position;
        SpeedInfo.text = "Speed: " + (rigidBody.velocity.magnitude).ToString("0.0");
        fuelInfo.text = "Fuel: " + fuelAmount.ToString("0.0");
        HPInfo.text = "Health: " + target.getHealth().ToString("0.0");
    }

    void Update()
    {
        rigidBody.interpolation = RigidbodyInterpolation.Interpolate;  
       
        if (!cameraController.CameraFreeMovement) orientationChange(io.mouse.x, io.mouse.y);           
        if (io.pushRight || io.pushLeft) SpaceshipMove(io.pushRight, io.pushLeft);
        if (io.steer.x != 0 || io.steer.y != 0)
        {
            Breaks(false);

            if (SpaceShipMovement(io.steer.x, io.steer.y) && io.steer.y < 0)
            {
                changeFlamesStates(true);
                cameraController.chromaticAberattion(true);
            }
            else
            {
                changeFlamesStates(false);
                cameraController.chromaticAberattion(false);
            }
        }
        else
        {
            if (fuelAmount < fuelMaxAmount)
            {
                fuelAmount += fuelRefeneration * Time.deltaTime;
                fuelInfo.text = "Fuel: " + fuelAmount.ToString("0.0");
                changeFlamesStates(false);
            }
            cameraController.chromaticAberattion(false);
        }

        if (Input.GetButtonDown("Fire1")) Fire();
        if (Input.GetButton("Fire2")) cameraController.CameraMovement();
        else
        {
            cameraController.CameraFreeMovement = false;
        }
	}

    void orientationChange(float ver, float hor){
        ver *= orientationChangeSensitivity;
        hor *= orientationChangeSensitivity;
        rigidBody.AddRelativeTorque(new Vector2(hor/2, ver/2), ForceMode.VelocityChange);
    }

    bool SpaceShipMovement(float Ver, float Hor)
    {

        if (fuelAmount > 0.1f)
        {
            
            if (Hor > 0)
            {
                Breaks(true);
            }
            else
            {
                if (rigidBody.velocity.magnitude > maxSpeed)rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
                else rigidBody.AddForce(transform.forward * Hor, ForceMode.VelocityChange);
            }

            rigidBody.AddTorque(transform.forward * Ver / 10, ForceMode.VelocityChange);
            fuelAmount -= fuelCombustion * Time.deltaTime;

            return true;
        }
        else
        {
            return false;
        }
    }
    bool SpaceshipMove(bool pushRight, bool pushLeft)
    {
        if (fuelAmount > fuelRefeneration)
        {
            if (rigidBody.velocity.magnitude > maxSpeed)
                rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
            else {
                if (pushRight) { rigidBody.AddForce(-transform.right * pushStrength, ForceMode.Impulse); fuelAmount -= fuelCombustion; } 
                if(pushLeft) { rigidBody.AddForce(transform.right * pushStrength, ForceMode.Impulse); fuelAmount -= fuelCombustion; }
            } 
            return true;
        }
        else return false;
    }

    void Fire()
    {
        Transform FirePoint;
        switch (lastFirePoint)
        {
            case 1: FirePoint = FirePoint2; lastFirePoint = 2; break;
            case 2: FirePoint = FirePoint1; lastFirePoint = 1; break;
            default: FirePoint = FirePoint2; lastFirePoint = 2; break;
        }
        GameObject bulletInstance = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(rigidBody.velocity, ForceMode.VelocityChange);
        RaycastHit hit;
        if (Physics.Raycast(bulletInstance.transform.position, bulletInstance.transform.forward, out hit))
        {
            Debug.DrawRay(bulletInstance.transform.position, bulletInstance.transform.forward, Color.green,20f,true);
        }
    }

    void Breaks(bool turn)
    {
        switch (turn)
        {
            case true:
                {
                    rigidBody.drag = normalDrag;
                    rigidBody.angularDrag = normalDrag;
                }
                break;
            case false:
                {
                    rigidBody.drag = minDrag;
                    rigidBody.angularDrag = minDrag;
                }
                break;
        }
    }

    void changeFlamesStates(bool turn)
    {
        if (turn && flame1.isStopped && flame2.isStopped && flame3.isStopped && flame4.isStopped)
        {
            flame1.Play();
            flame2.Play();
            flame3.Play();
            flame4.Play();
        }
        else if (!turn)
        {
            flame1.Stop();
            flame2.Stop();
            flame3.Stop();
            flame4.Stop();
        }
    }
}
