using MainGame.Utilities;

namespace MainGame.CharacterResources.Interfaces
{
    public interface IHealth
    {
        float Current { get; }
        float Max { get; }
        event ValueChangedDelegate<float> OnValueChanged;
        void TakeDamage(float amount);
        void Heal(float amount);
    }
}