using UnityEngine;

namespace MainGame.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/CharacterStats")]
    public class CharacterStatsSO : ScriptableObject
    {
        [field: SerializeField] public float Health;
        [field: SerializeField] public float Damage;
        [field: SerializeField] public float MovementSpeed;
    }
}
