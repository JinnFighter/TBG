namespace Visuals.Characters
{
    public class BattleCharacterController : BaseController<CharacterModel, CharacterView>
    {
        public BattleCharacterController(CharacterModel model, CharacterView view) : base(model, view)
        {
        }
    }
}