using UnityEngine;

public class TestUP : MonoBehaviour
{
    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }
}
