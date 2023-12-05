namespace Snake
{
    public interface ISnakeState
    {
        void Enter(SnakeController snakeController);
        void Execute(SnakeController snake);
        void Exit(SnakeController snake);
    }
}