using Logic;
using UnityEngine;
using Visuals;

namespace Init
{
    public class InitGame : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private IVisualizerService _visualizerService;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine();
            _visualizerService = new VisualizerService();
        }

        private void Start()
        {
            _gameStateMachine.Init();
            _visualizerService.Init(_gameStateMachine);
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