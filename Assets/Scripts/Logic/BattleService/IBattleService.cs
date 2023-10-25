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

        void Init(List<CharacterInfo> characterInfos);
        void Terminate();
        void StartBattle();
        IActionSubmitter ActionSubmitter { get; }
        IActionProcessor ActionProcessor { get; }
        CharactersContainer CharactersContainer { get; }
    }
}