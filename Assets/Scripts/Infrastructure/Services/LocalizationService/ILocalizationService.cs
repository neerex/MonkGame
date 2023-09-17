using Cysharp.Threading.Tasks;

namespace MainGame.Infrastructure.Services.LocalizationService
{
    public interface ILocalizationService
    {
        UniTask Initialize();
        string Translate(string key);
    }
}