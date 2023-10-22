using System.Collections.Generic;
using Logic.Actions;
using Logic.Characters;
using Logic.TurnSteps;
using UnityEngine.Events;

namespace Logic.BattleService
{
    public interface IBattleService
    {
        bool IsBattleStarted { get; }
        bool IsBattleFinished { get; }

        UnityEvent<ETurnStep> OnTurnStepEnter { get; }

        void Init();
        void Terminate();
        void StartBattle(List<CharacterInfo> characterInfos);
        IActionSubmitter ActionSubmitter { get; }
        IActionProcessor ActionProcessor { get; }
        CharactersContainer CharactersContainer { get; }
    }
}