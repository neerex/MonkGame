using UnityEngine;

namespace MainGame.Abilities.Spells
{
    public class SpellCastInfo
    {
        public GameObject Source;
        public RaycastHit ClickedPosition;
        public Transform CasterHands;
        public LayerMask LayerToDamage;
    }
}
