using System.Collections.Generic;
using MainGame.ScriptableConfigs;
using UnityEngine;
using Zenject;

namespace MainGame.Installers
{
    [CreateAssetMenu(fileName = "GameConfigsInstaller", menuName = "Installers/GameConfigsInstaller")]
    public class GameConfigsInstaller : ScriptableObjectInstaller<GameConfigsInstaller>
    {
        [SerializeField] private LayerMaskConfigSO _layerMaskConfig;
        [SerializeField] private SpellConfigListSO _spellConfigList;

        public override void InstallBindings()
        {
            Container.BindInstance(_layerMaskConfig);
            Container.BindInstance(_spellConfigList);
        }
    }
}
