using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.CompareTag("Collection"))
        {
            // todo: 如何根据物体受力大小来判断开门角度？


            // transform.Rotate(0, 10, 0);
        }
    }
}