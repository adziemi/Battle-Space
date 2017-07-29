using UnityEngine;

public class HyperSpeed : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce(-other.gameObject.transform.forward * 100, ForceMode.Impulse);
            Debug.Log("Collision");

        }
    }
}
