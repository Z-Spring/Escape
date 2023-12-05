using UnityEngine;
using UnityEngine.AI;

namespace Snake
{
    public sealed class PatrolState : ISnakeState
    {
        public static readonly PatrolState Instance = new PatrolState();

        private PatrolState()
        {
        }

        public void Enter(SnakeController snakeController)
        {
        }

        public void Execute(SnakeController snake)
        {
            NavMeshAgent agent = snake.agent;
            int currentPatrolPointIndex = snake.currentPatrolPointIndex;
            snake.SnakeMoveTowards(snake.patrolPoints[currentPatrolPointIndex].position);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Debug.Log("Reached Patrol Point");
                snake.currentPatrolPointIndex = (currentPatrolPointIndex + 1) % snake.patrolPoints.Count;
            }

            if (snake.HasFoundPlayer())
            {
                Debug.Log("Found Player");
                snake.SetSnakeState(ChaseState.Instance);
                return;
            }

            if (snake.HasFoundFood())
            {
                Debug.Log("Found Food");
                snake.SetSnakeState(GetFoodState.Instance);
            }
        }

        public void Exit(SnakeController snake)
        {
        }
    }
}