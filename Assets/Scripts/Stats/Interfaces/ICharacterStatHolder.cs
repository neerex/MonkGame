namespace MainGame.Stats.Interfaces
{
    public interface ICharacterStatHolder
    {
        void InitializeStatLibrary();
        bool GetStat<T>(out T stat);
    }
}