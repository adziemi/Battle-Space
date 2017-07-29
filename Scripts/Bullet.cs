using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Blast;
    public float speed = 1f;
    public float damage = 1f;

    private Rigidbody rigidBody;
    
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        if (gameObject != null) Destroy(gameObject,5f);
        rigidBody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.collider.gameObject;
        
        if(collider.GetComponent<Target>() != null)
        {
            Target target = collider.GetComponent<Target>();
            target.takeDamage(damage);

            if (target.isDead()) {
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
                Destroy(collision.collider.gameObject);
           } 
        }
        
        Destroy(Instantiate(Blast, gameObject.transform.position, gameObject.transform.rotation),2f);
        Destroy(gameObject);
    }


}
