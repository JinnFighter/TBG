namespace Logic
{
    public interface IGameState
    {
        EGameState Id { get; }
        void EnterState(GameStateMachine gameStateMachine);
    }
}