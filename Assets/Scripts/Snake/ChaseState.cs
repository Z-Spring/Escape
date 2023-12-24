using UnityEngine;

namespace Snake
{
    public sealed class ChaseState : ISnakeState
    {
        public static readonly ChaseState Instance = new ChaseState();

        private ChaseState()
        {
        }

        public void Enter(SnakeController snakeController)
        {
            snakeController.agent.speed = snakeController.chaseSpeed;
            snakeController.bodyFollow.followSpeed = snakeController.bodyFollowChaseSpeed;
        }

        public void Execute(SnakeController snake)
        {
            Vector3 playerPosition = snake.player.position;
            snake.SnakeMoveTowards(playerPosition);
            float distance = Vector3.Distance(snake.head.position, playerPosition);
            if (distance > snake.chaseDistance)
            {
                snake.SetSnakeState(PatrolState.Instance);
                return;
            }

            if (distance <= snake.snakeAttackDistance)
            {
                snake.SetSnakeState(AttackState.Instance);
                return;
            }

            if (snake.HasFoundFood())
            {
                if (PlayerController.Instance.HasThrownFood)
                {
                    PlayerController.Instance.HasThrownFood = false;
                    snake.SetSnakeState(GetFoodState.Instance);
                }
            }
        }

        public void Exit(SnakeController snake)
        {
            snake.agent.speed = snake.patrolSpeed;
            snake.bodyFollow.followSpeed = snake.bodyFollowPatrolSpeed;
        }
    }
}