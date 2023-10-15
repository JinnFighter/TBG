using System;
using System.Collections.Generic;
using Logic.Actions;
using Logic.Characters;
using UnityEngine;
using UnityEngine.Events;
using CharacterInfo = Logic.Characters.CharacterInfo;

namespace Logic
{
    public class GameStateMachine
    {
        private readonly ActionProcessor _actionProcessor = new();
        private readonly Dictionary<Type, IGameState> _gameStates = new();

        public readonly UnityEvent<ActionInfo> OnActionSubmitted = new();
        public readonly UnityEvent OnVisualizationFinished = new();
        public readonly UnityEvent<ActionInfo, ActionResultContainer> OnVisualizationStarted = new();

        private IGameState _currentGameState;

        private ActionInfo _lastActionInfo;

        public CharactersContainer CharactersContainer { get; } = new();

        public ActionResultContainer LastActionResult { get; private set; }
        public EGameState CurrentState => _currentGameState?.Id ?? EGameState.Invalid;

        public void Init()
        {
            _gameStates.Add(typeof(SelectTeamGameState), new SelectTeamGameState());
            _gameStates.Add(typeof(AwaitingInputGameState), new AwaitingInputGameState());
            _gameStates.Add(typeof(ProcessActionsState), new ProcessActionsState());
            _gameStates.Add(typeof(VisualizeActionsState), new VisualizeActionsState());

            CharactersContainer.Init(new List<CharacterInfo>
            {
                new(0, "Player", CharactersContainer.PlayerTeamId),
                new(1, "Enemy", CharactersContainer.EnemyTeamId)
            });

            _actionProcessor.Init();
        }

        public void Terminate()
        {
            ResetSubmittedAction();
            _actionProcessor.Terminate();
            CharactersContainer.Terminate();
            _gameStates.Clear();
        }

        public void SetGameState<TGameState>() where TGameState : IGameState
        {
            if (!_gameStates.TryGetValue(typeof(TGameState), out var gameState)) return;

            _currentGameState = gameState;
            _currentGameState?.EnterState(this);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && CharactersContainer.CurrentTeamId == CharactersContainer.PlayerTeamId)
                TrySubmitAction(new ActionInfo
                {
                    ActionId = "test",
                    CasterId = 0
                });
        }

        public void ResetSubmittedAction()
        {
            _lastActionInfo = null;
            LastActionResult = null;
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
            if (_lastActionInfo != null) LastActionResult = _actionProcessor.ProcessAction(_lastActionInfo);
        }

        public void VisualizeAction()
        {
            if (LastActionResult != null) OnVisualizationStarted.Invoke(_lastActionInfo, LastActionResult);
        }
    }
}