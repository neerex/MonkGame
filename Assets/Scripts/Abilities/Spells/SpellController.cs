using System;
using MainGame.Player.Animation;
using MainGame.Services.Input.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Logger = MainGame.Utilities.Logger;


namespace MainGame.Abilities.Spells
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class SpellController : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        private IPlayerInputService _inputService;
        
        private readonly int[] _spellBook = new int[6];
        private int _currentSpellIndexToCast = -1;

        public event Action<int[]> OnSpellPositionInTheBookChanged;

        [Inject]
        public void Construct(IPlayerInputService inputService)
        {
            _inputService = inputService;

            _inputService.OnMainAttackPerformed += CastMainSpell;
            _inputService.OnSecondaryAttackPerformed += CastSecondarySpell;
            _inputService.OnAttack1Performed += CastAttack1Spell;
        }
        
        private void Awake()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
        }

        private void OnDestroy()
        {
            _inputService.OnMainAttackPerformed -= CastMainSpell;
            _inputService.OnSecondaryAttackPerformed -= CastSecondarySpell;
            _inputService.OnAttack1Performed -= CastAttack1Spell;
        }

        private void CastMainSpell(InputAction.CallbackContext context)
        {
            _currentSpellIndexToCast = 0;
            _playerAnimator.PlayMainAttack();
        }

        private void CastSecondarySpell(InputAction.CallbackContext context)
        {
            _currentSpellIndexToCast = 1;
            _playerAnimator.PlaySecondaryAttack();
        }

        private void CastAttack1Spell(InputAction.CallbackContext context)
        {
            _currentSpellIndexToCast = 2;
            _playerAnimator.PlayAttackSlot1();
        }

        //animation event
        public void CastSpell()
        {
            Logger.Log($"Cast Spell Number {_currentSpellIndexToCast}", Color.blue);
        }

        public void ChangeSpellPlaces(int indexFrom, int indexTo)
        {
            (_spellBook[indexFrom], _spellBook[indexTo]) = (_spellBook[indexTo], _spellBook[indexFrom]);
            OnSpellPositionInTheBookChanged?.Invoke(_spellBook);
        }
    }
}
