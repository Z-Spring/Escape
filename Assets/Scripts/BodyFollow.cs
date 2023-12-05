using System.Collections.Generic;
using UnityEngine;

public class BodyFollow : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();
    public float followSpeed = 2f;
    public SnakeController snake;
    [SerializeField] private Transform snakeHead;

    void Update()
    {
        BodyMovement();
    }

    private void BodyMovement()
    {
        if (snake.IsMoving)
        {
            for (int i = bodyParts.Count - 1; i >= 0; i--)
            {
                Transform target = i == 0 ? snakeHead.GetChild(0) : bodyParts[i - 1].GetChild(0);
                bodyParts[i].position = Vector3.Lerp(bodyParts[i].position, target.position,
                    followSpeed * Time.deltaTime);
                bodyParts[i].rotation = Quaternion.Slerp(bodyParts[i].rotation, target.rotation,
                    followSpeed * Time.deltaTime);
            }
        }
    }
}