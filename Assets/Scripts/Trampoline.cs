using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounce = 20f;
    public int bounceCount = 3;

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            // this ia a test
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * bounce, ForceMode.Impulse);
        }
    }

   
}