using System;
using System.Collections.Generic;

namespace Logic
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IGameState> _gameStates = new();

        private IGameState _currentGameState;
        public EGameState CurrentState => _currentGameState?.Id ?? EGameState.Invalid;

        public void Init()
        {
            _gameStates.Add(typeof(SelectTeamGameState), new SelectTeamGameState());
            _gameStates.Add(typeof(AwaitingInputGameState), new AwaitingInputGameState());
            _gameStates.Add(typeof(ProcessActionsState), new ProcessActionsState());
            _gameStates.Add(typeof(VisualizeActionsState), new VisualizeActionsState());
        }

        public void SetGameState<TGameState>() where TGameState : IGameState
        {
            if (!_gameStates.TryGetValue(typeof(TGameState), out var gameState)) return;

            _currentGameState = gameState;
            _currentGameState?.EnterState(this);
        }
    }
}