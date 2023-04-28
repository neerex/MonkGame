using System.Collections;
using MainGame.Player.Animation;
using MainGame.Services.Input.Interfaces;
using MainGame.Services.Raycast.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Logger = MainGame.Utilities.Logger;


namespace MainGame.Abilities.Spells
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class SpellController : MonoBehaviour, ISpellBookHolder
    {
        [SerializeField] private Transform _placeToCastFrom;
        [SerializeField] private PlayerAnimator _playerAnimator;
        
        private IPlayerInputService _inputService;
        private IMouseRaycastService _mouseRaycastService;

        private Spell _currentlyCastingSpell;
        private int _currentSpellIndexToCast = -1;
        public SpellBook SpellBook { get; private set; }
        
        [Inject]
        public void Construct(IPlayerInputService inputService, IMouseRaycastService mouseRaycastService)
        {
            _inputService = inputService;
            _mouseRaycastService = mouseRaycastService;
            
            _inputService.OnMainAttackPerformed += CastMainSpell;
            _inputService.OnSecondaryAttackPerformed += CastSecondarySpell;
            _inputService.OnAttack1Performed += CastAttack1Spell;
        }
        
        private void Awake()
        {
            SpellBook = new SpellBook(6);
            _playerAnimator = GetComponent<PlayerAnimator>();
        }

        private void OnDestroy()
        {
            _inputService.OnMainAttackPerformed -= CastMainSpell;
            _inputService.OnSecondaryAttackPerformed -= CastSecondarySpell;
            _inputService.OnAttack1Performed -= CastAttack1Spell;
        }

        //animation event
        public IEnumerator CastSpell()
        {
            if(_currentlyCastingSpell != null && _currentlyCastingSpell.IsCasting) yield break;

            _currentlyCastingSpell = SpellBook[_currentSpellIndexToCast];
            
            SpellCastInfo spellCastInfo = new SpellCastInfo()
            {
                CasterHandsForward = _placeToCastFrom
            };
            yield return _currentlyCastingSpell.BeginCast(spellCastInfo);
            
            _currentlyCastingSpell = null;
            
            Logger.Log($"Cast Spell Number {_currentSpellIndexToCast}", Color.blue);
        }

        private void CastMainSpell(InputAction.CallbackContext context)
        {
            _currentSpellIndexToCast = 0;
            TryCastSpell(_currentSpellIndexToCast);
        }

        private void CastSecondarySpell(InputAction.CallbackContext context)
        {
            _currentSpellIndexToCast = 1;
            TryCastSpell(_currentSpellIndexToCast);
        }

        private void CastAttack1Spell(InputAction.CallbackContext context)
        {
            _currentSpellIndexToCast = 2;
            TryCastSpell(_currentSpellIndexToCast);
        }

        private void TryCastSpell(int spellBookIndex)
        {
            if (spellBookIndex >= SpellBook.Capacity)
            {
                Logger.Log("Invalid Index in SpellBook");
                return;
            }
            
            Spell spellToCast = SpellBook[spellBookIndex];
            if (spellToCast != null)
            {
                _playerAnimator.PlayerAnimationWithHash(spellToCast.SpellConfig.AnimationHash);
            }
        }
    }
}
