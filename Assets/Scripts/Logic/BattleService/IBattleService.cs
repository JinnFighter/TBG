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
        UnityEvent OnTurnEnd { get; }
        UnityEvent<ActionInfo, ActionResultContainer> OnActionProcessingFinished { get; }
        IActionSubmitter ActionSubmitter { get; }
        CharactersContainer CharactersContainer { get; }

        void Init(List<CharacterInfo> characterInfos);
        void Terminate();
        void StartBattle();
    }
}