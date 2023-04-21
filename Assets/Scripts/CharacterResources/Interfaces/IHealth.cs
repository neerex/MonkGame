using MainGame.Utilities;

namespace MainGame.CharacterResources.Interfaces
{
    public interface IHealth
    {
        float Current { get; }
        float Max { get; }
        
        /// <summary> <b>Old value</b> and <b>NewValue</b> </summary>
        event ValueChangedDelegate<float> ValueChanged;
        
        void TakeDamage(float amount);
        void Heal(float amount);
    }
}