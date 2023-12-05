namespace Snake
{
    public sealed class AttackState : ISnakeState
    {
        public static readonly AttackState Instance = new AttackState();
        
        private AttackState()
        {
        }
        
        public void Enter(SnakeController snakeController)
        {
            
        }

        public void Execute(SnakeController snake)
        {
            snake.agent.isStopped = true;
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
        }

        public void Exit(SnakeController snake)
        {
            
        }
    }
}