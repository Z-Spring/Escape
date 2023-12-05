using System.Collections;
using UnityEngine;

namespace Snake
{
    public class HandleSparePlayer : MonoBehaviour
    {
        [SerializeField] private SnakeController snakeController;
        [SerializeField] private float randomValue;
        [SerializeField] private float turnSpeed = 10f;
        [Range(0,1)][SerializeField] private float sparePlayerRate = 0.3f;
        internal bool ShouldSparePlayer()
        {
            randomValue = Random.value;

            return randomValue <= sparePlayerRate;
        }

        internal void SparePlayer()
        {
            // StartCoroutine(LookAtPlayer());
            PrintTipMessage.Instance.SetPrintMessageInfos("I'll spare you this time", snakeController.waitText);
            snakeController.hasSparePlayer = true;
            StartCoroutine(SparePlayerTimer());
        }

        private IEnumerator SparePlayerTimer()
        {
            yield return new WaitForSeconds(snakeController.sparePlayerTimer);
            snakeController.hasSparePlayer = false;
        }

        private IEnumerator LookAtPlayer()
        {
            Transform head = snakeController.head;
            Quaternion originalRotation = head.rotation;
            Vector3 relativePos = snakeController.player.position - head.position;

            Quaternion targetRotation = Quaternion.LookRotation(relativePos);
            float targetRotationY = targetRotation.eulerAngles.y;
            float originalRotationY = originalRotation.eulerAngles.y;
            float currentAngle = Mathf.LerpAngle(originalRotationY, targetRotationY, Time.deltaTime * turnSpeed);
            Debug.Log("currentAngle: " + currentAngle);
            head.rotation = Quaternion.Euler(0, currentAngle, 0);
            // head.LookAt(snakeController.player.transform.position);
            yield return new WaitForSeconds(4f);
            head.rotation = originalRotation;
        }

        internal IEnumerator EatFoodCoroutine()
        {
            snakeController.food.GetComponent<Collection>().HasEatFood = false;
            snakeController.agent.isStopped = true;
            snakeController.agent.velocity = Vector3.zero;
            yield return new WaitForSeconds(snakeController.snakeEatTime);
            // snakeState = SnakeState.Patrol;
            // SetSnakeState(new PatrolState());
            snakeController.agent.isStopped = false;
        }
    }
}