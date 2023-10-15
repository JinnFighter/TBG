using System;
using System.Collections.Generic;
using Logic.Actions;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class GameStateMachine
    {
        private readonly ActionProcessor _actionProcessor = new();
        private readonly Dictionary<Type, IGameState> _gameStates = new();

        public readonly UnityEvent<ActionInfo> OnActionSubmitted = new();

        private IGameState _currentGameState;

        private ActionInfo _lastActionInfo;
        public EGameState CurrentState => _currentGameState?.Id ?? EGameState.Invalid;

        public void Init()
        {
            _gameStates.Add(typeof(SelectTeamGameState), new SelectTeamGameState());
            _gameStates.Add(typeof(AwaitingInputGameState), new AwaitingInputGameState());
            _gameStates.Add(typeof(ProcessActionsState), new ProcessActionsState());
            _gameStates.Add(typeof(VisualizeActionsState), new VisualizeActionsState());

            _actionProcessor.Init();
        }

        public void Terminate()
        {
            _actionProcessor.Terminate();

            _gameStates.Clear();
            ResetSubmittedAction();
        }

        public void SetGameState<TGameState>() where TGameState : IGameState
        {
            if (!_gameStates.TryGetValue(typeof(TGameState), out var gameState)) return;

            _currentGameState = gameState;
            _currentGameState?.EnterState(this);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TrySubmitAction(new ActionInfo
                {
                    ActionId = "test",
                    CasterId = 0
                });
        }

        public void ResetSubmittedAction()
        {
            _lastActionInfo = null;
        }

        public bool TrySubmitAction(ActionInfo actionInfo)
        {
            if (_lastActionInfo != null) return false;

            _lastActionInfo = actionInfo;
            OnActionSubmitted.Invoke(_lastActionInfo);
            return true;
        }

        public void ProcessAction()
        {
            if (_lastActionInfo != null) _actionProcessor.ProcessAction(_lastActionInfo);
        }
    }
}