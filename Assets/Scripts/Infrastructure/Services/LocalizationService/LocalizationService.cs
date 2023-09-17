using Cysharp.Threading.Tasks;
using MainGame.Utilities;

namespace MainGame.Infrastructure.Services.LocalizationService
{
    public class LocalizationService : ILocalizationService
    {
        public async UniTask Initialize()
        {
            // load and parse your localization data base here
            Logger.LogWarning("Localization service isn't implemented yet");
            await UniTask.WaitForSeconds(0.1f);
        }

        public string Translate(string key)
        {
            // localization logic
            return key;
        }
    }
}