using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour {

    public GameObject bullet;
    public Transform firePoint;
    private Rigidbody rigidBody;
    
    [Header("Parameters")]
    public float maxSpeed = 10f;
    public float reloadRate = 0.5f;

    [Header("Internal")]
    public bool allowShoot = true;
    

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }
    public bool moveForeward(float velocity) {

        if (rigidBody.velocity.magnitude > maxSpeed)
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, 10f);
        else rigidBody.AddForce(transform.forward * velocity, ForceMode.VelocityChange);
        
        return true;
    }
    public void breaks(bool turn) {
        switch (turn)
        {
            case true:
                {
                    rigidBody.drag = 0.5f;
                    rigidBody.angularDrag = 0.5f;
                }
                break;
            case false:
                {
                    rigidBody.drag = 0f;
                    rigidBody.angularDrag = 0f;
                }
                break;
        }
    }
    public void rotate(Vector3 direction) {
        float lerpSpeed = 5f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, lerpSpeed * Time.deltaTime).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
    public void Fire()
    {
        GameObject bulletInstance = Instantiate(bullet, firePoint.position, firePoint.rotation);
        RaycastHit hit;

        if (Physics.Raycast(bulletInstance.transform.position, bulletInstance.transform.forward, out hit))
        {
        }
        allowShoot = false;
    }
    public IEnumerator reload()
    {
        yield return new WaitForSeconds(Time.deltaTime + reloadRate);
        allowShoot = true;
    }
}
