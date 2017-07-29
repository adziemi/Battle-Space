using UnityEngine;

public class RockFracture : MonoBehaviour {
    public GameObject fracturedRock;
    private Target target;
	
	void Start () {
        target = gameObject.GetComponent<Target>();
	}
	
    private void OnCollisionEnter(Collision collision)
    {
        if (target.isDead()) {
            GameObject fracturedRockReference = Instantiate(fracturedRock, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            Destroy(fracturedRockReference, 30f);
            
        }
    }
}
