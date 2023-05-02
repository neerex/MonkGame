using UnityEngine;

namespace MainGame.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "LayerMasksConfigs", menuName = "LayerMasksConfigs")]
    public class LayerMaskConfigSO : ScriptableObject
    {
        [field: SerializeField] public LayerMask LevelSurroundings { get; private set; }
        [field: SerializeField] public LayerMask HitableByPlayer { get; private set; }
        [field: SerializeField] public LayerMask Player { get; private set; }
        [field: SerializeField] public LayerMask Landing { get; private set; } 
    }
}
