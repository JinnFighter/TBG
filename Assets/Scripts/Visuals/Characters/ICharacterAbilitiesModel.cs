using System.Collections.Generic;

namespace Visuals.Characters
{
    public interface ICharacterAbilitiesModel : IModel
    {
        List<ICharacterAbilityModel> Abilities { get; }
    }
}