using UnityEngine;

namespace MainGame.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "LayerMasksConfigs", menuName = "LayerMasksConfigs")]
    public class LayerMaskConfigSO : ScriptableObject
    {
        [field: SerializeField] public LayerMask LevelSurroundings { get; private set; }
        [field: SerializeField] public LayerMask Enemies { get; private set; }
        [field: SerializeField] public LayerMask Player { get; private set; }
        [field: SerializeField] public LayerMask Ground { get; private set; }
    }
}
