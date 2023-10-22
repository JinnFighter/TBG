using System;
using System.Collections.Generic;
using UnityEngine;
using Visuals.UiService;

namespace Visuals.Characters
{
    [CreateAssetMenu(menuName = "Create CharacterViewContainer", fileName = "CharacterViewContainer", order = 0)]
    public class CharacterViewContainer : BaseViewContainer
    {
        [SerializeField] private PlayerCharacterView _playerCharacterView;
        [SerializeField] private EnemyCharacterView _enemyCharacterView;

        public override void Init()
        {
            Views = new Dictionary<Type, BaseView>
            {
                {typeof(PlayerCharacterView), _playerCharacterView},
                {typeof(EnemyCharacterView), _enemyCharacterView}
            };
        }
    }
}