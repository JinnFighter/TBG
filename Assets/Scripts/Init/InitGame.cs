using Logic;
using UnityEngine;

namespace Init
{
    public class InitGame : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine();
        }

        private void Start()
        {
            _gameStateMachine.Init();
            _gameStateMachine.SetGameState<SelectTeamGameState>();
        }

        private void Update()
        {
            _gameStateMachine.Update();
        }

        private void OnDestroy()
        {
            _gameStateMachine.Terminate();
        }
    }
}