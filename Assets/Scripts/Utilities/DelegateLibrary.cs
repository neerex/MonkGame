namespace MainGame.Utilities
{
    /// <summary>
    /// Creates delegate that perform calculations with old value and new value.
    /// </summary>
    /// <param name="oldValue"> Value before it changed to new value</param>
    /// <param name="newValue"> Current new value</param>
    public delegate void ValueChangedDelegate<in T>(T oldValue, T newValue);
    
    public class DelegateLibrary { }
}