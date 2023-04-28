namespace MainGame.Stats.Interfaces
{
    public interface IStatHolder
    {
        void InitializeStatLibrary();
        bool GetStat<T>(out T stat);
    }
}