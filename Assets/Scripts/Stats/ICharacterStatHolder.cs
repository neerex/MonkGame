namespace MainGame.Stats
{
    public interface ICharacterStatHolder
    {
        bool GetStat<T>(out T stat);
    }
}