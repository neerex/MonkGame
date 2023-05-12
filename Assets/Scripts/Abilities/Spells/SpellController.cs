using System.Collections;
using MainGame.Entities.Player.Animation;
using MainGame.Infrastructure.Services.Input.Interfaces;
using MainGame.Infrastructure.Services.Raycast.Interfaces;
using MainGame.ScriptableConfigs;
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
        private LayerMaskConfigSO _layerMasks;

        private Spell _currentlyCastingSpell;
        private SpellCastInfo _spellCastInfo;
        private int _currentSpellIndexToCast = -1;
        public SpellBook SpellBook { get; private set; }
        
        [Inject]
        public void Construct(IPlayerInputService inputService, IMouseRaycastService mouseRaycastService, LayerMaskConfigSO layerMaskConfig)
        {
            _inputService = inputService;
            _mouseRaycastService = mouseRaycastService;
            _layerMasks = layerMaskConfig;
            
            _inputService.OnMainAttackPerformed += CastMainSpell;
            _inputService.OnSecondaryAttackPerformed += CastSecondarySpell;
            _inputService.OnAttack1Performed += CastAttack1Spell;
        }
        
        private void Awake()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
            SpellBook = new SpellBook(6);
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
            if(_currentlyCastingSpell != null) yield break;

            _currentlyCastingSpell = SpellBook[_currentSpellIndexToCast];
            
            yield return _currentlyCastingSpell.BeginCast(_spellCastInfo);
            
            _currentlyCastingSpell = null;
            
            Logger.Log($"Cast Spell Number {_currentSpellIndexToCast}", Color.blue);
        }

        private void CastMainSpell(InputAction.CallbackContext context) => TryCastSpell(0);
        private void CastSecondarySpell(InputAction.CallbackContext context) => TryCastSpell(1);
        private void CastAttack1Spell(InputAction.CallbackContext context) => TryCastSpell(2);

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
                _spellCastInfo = CollectCastInfo();
                _currentSpellIndexToCast = spellBookIndex;
                _playerAnimator.PlayAnimationWithHash(spellToCast.SpellConfig.AnimationHash);
            }
        }

        private SpellCastInfo CollectCastInfo()
        {
            return new SpellCastInfo
            {
                Source = gameObject,
                CasterHands = _placeToCastFrom,
                ClickedPosition = _mouseRaycastService.GetPointOnSurface(_layerMasks.LevelSurroundings),
                LayerToDamage = _layerMasks.HitableByPlayer
            };
        }
    }
}
