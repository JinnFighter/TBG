using System.Collections.Generic;
using Logic.TurnSteps;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class GameStateMachine
    {
        private readonly List<IGameStep> _gameSteps = new();
        private readonly TurnContext _turnContext = new();

        private int _currentStepIndex = -1;
        public UnityEvent<EGameStep> OnStateEnter { get; } = new();

        public void Init(List<IGameStep> gameSteps)
        {
            _gameSteps.Clear();
            _gameSteps.AddRange(gameSteps);
        }

        public void Terminate()
        {
            var currentStep = _gameSteps[_currentStepIndex];
            currentStep.ExitStep(_turnContext);
            currentStep.StepCompleted.RemoveListener(HandleStepCompleted);
            _currentStepIndex = -1;
            _gameSteps.Clear();
        }

        public void GoToNextState()
        {
            var nextStateIndex = GetNextStepIndex();
            _currentStepIndex = nextStateIndex;
            var nextStep = _gameSteps[_currentStepIndex];
            nextStep.StepCompleted.AddListener(HandleStepCompleted);
            Debug.Log($"Enter step: {_gameSteps[_currentStepIndex].Id}");
            nextStep.EnterStep(_turnContext);
            OnStateEnter.Invoke(nextStep.Id);
            Debug.Log($"Do step: {_gameSteps[_currentStepIndex].Id}");
            nextStep.DoStep(_turnContext);
        }

        private void HandleStepCompleted()
        {
            var currentStep = _gameSteps[_currentStepIndex];
            currentStep.StepCompleted.RemoveListener(HandleStepCompleted);
            Debug.Log($"Exit step: {currentStep.Id}");
            currentStep.ExitStep(_turnContext);
            GoToNextState();
        }

        private int GetNextStepIndex()
        {
            var nextStateIndex = _currentStepIndex < 0 ? 0 :
                _currentStepIndex < _gameSteps.Count - 1 ? _currentStepIndex + 1 : 0;
            return nextStateIndex;
        }
    }
}