using UnityEngine;

namespace Snake
{
    public class GetFoodState : ISnakeState
    {
        public static  readonly GetFoodState Instance = new GetFoodState();
        
        private GetFoodState()
        {
        }
        
        private HandleSparePlayer handleSparePlayer;

        public void Enter(SnakeController snakeController)
        {
        }
        
        public void Execute(SnakeController snake)
        {
            handleSparePlayer = snake.GetComponent<HandleSparePlayer>();
            // Debug.Log("MoveToFood");

            if (snake.HasFoundPlayer() && !snake.hasSparePlayer)
            {
                if (handleSparePlayer.ShouldSparePlayer())
                {
                    Debug.Log("Spare Player");
                    handleSparePlayer.SparePlayer();
                }
                else
                {
                    SwitchToChaseState(snake);
                    return;
                }
            }
            
            if (HasEatenFood(snake))
            {
                Debug.Log("Eaten Food");
                SwitchToPatrolState(snake);
            }
            else
            {
                Debug.Log("Move To Food");
                snake.SnakeMoveTowards(snake.food.transform.position);
            }
        }


        //------------------//

        private void SwitchToChaseState(SnakeController snake)
        {
            snake.SetSnakeState(ChaseState.Instance);
            snake.hasSparePlayer = false;
        }

        private void SwitchToPatrolState(SnakeController snake)
        {
            PrintTipMessage.Instance.SetPrintMessageInfos("You have 5 seconds to escape...", snake.waitText);
            snake.StartCoroutine(handleSparePlayer.EatFoodCoroutine());
            snake.SetSnakeState(PatrolState.Instance);
        }

        private bool HasEatenFood(SnakeController snake)
        {
            return snake.food.GetComponent<Collection>().HasEatFood;
        }


        public void Exit(SnakeController snake)
        {
        }
    }
}