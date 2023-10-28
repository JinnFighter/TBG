using TMPro;
using UnityEngine;

namespace Visuals.Ui.Hud
{
    public class CharacterStatsHudView : UiView
    {
        [field: SerializeField] public TextMeshProUGUI TextCharacterName { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TextCurrentHealth { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TextMaxHealth { get; private set; }
    }
}