using UnityEngine;
using Logger = MainGame.Utilities.Logger;


namespace MainGame.Abilities.Spells
{
    public class SpellController : MonoBehaviour
    {
        public void CastSpell()
        {
            Logger.Log("Cast Spell", Color.cyan);
        }
    }
}
