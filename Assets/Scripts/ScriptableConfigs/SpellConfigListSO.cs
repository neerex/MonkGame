using System.Collections.Generic;
using UnityEngine;

namespace MainGame.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "SpellConfigList", menuName = "SpellConfigList")]
    public class SpellConfigListSO : ScriptableObject
    {
        public List<SpellConfigSO> SpellConfigs = new();
    }
}
