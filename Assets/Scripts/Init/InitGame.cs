using System.Collections.Generic;
using Logic;
using Logic.BattleService;
using Logic.Characters;
using Logic.Config;
using UnityEngine;
using Visuals;
using Visuals.BattleArena;
using Visuals.Characters;
using Visuals.UiService;
using CharacterInfo = Logic.Characters.CharacterInfo;

namespace Init
{
    public class InitGame : MonoBehaviour
    {
        [SerializeField] private UiService _uiService;

        [SerializeField] private BattleArenaSceneData _battleArenaSceneData;
        [SerializeField] private CharacterViewContainer _characterViewContainer;

        [SerializeField] private AttackAbilityConfig attackAbilityConfig;
        private BattleEntryPoint _battleEntryPoint;

        private IBattleService _battleService;

        private IVisualizerService _visualizerService;

        private void Awake()
        {
            _battleService = new BattleService();
            _visualizerService = new VisualizerService(_battleService.ActionProcessor);
        }

        private void Start()
        {
            _battleService.Init(new List<CharacterInfo>
            {
                new(new CharacterData(0, "Player", ECharacterTeam.Player), new CharacterStats(10, 10),
                    new CharacterAbilities(
                        new List<BaseAbilityConfig>
                        {
                            attackAbilityConfig
                        })),
                new(new CharacterData(1, "Enemy", ECharacterTeam.Enemy), new CharacterStats(10, 10),
                    new CharacterAbilities(
                        new List<BaseAbilityConfig>
                        {
                            attackAbilityConfig
                        }))
            });

            _battleService.StartBattle();

            _characterViewContainer.Init();

            _uiService.Init();

            _battleEntryPoint = new BattleEntryPoint(_battleArenaSceneData, _battleService, _visualizerService,
                _characterViewContainer, _uiService);
            _battleEntryPoint.Init();
        }

        private void OnDestroy()
        {
            _battleEntryPoint.Terminate();
            _uiService.Terminate();

            _battleService.Terminate();
        }
    }
}