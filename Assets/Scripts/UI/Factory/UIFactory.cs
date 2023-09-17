using MainGame.Infrastructure.Services.Asset.Interfaces;
using MainGame.Infrastructure.Services.StaticData;
using MainGame.ScriptableConfigs;
using MainGame.StaticData;
using MainGame.UI.GameWindows;
using UnityEngine;

namespace MainGame.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInjectedAssetProvider _injectedAssetProvider;
        private readonly IAssetProvider _assetProvider;
        
        private UISorter _uiSorter;
        
        public UIFactory(IStaticDataService staticDataService, IInjectedAssetProvider injectedAssetProvider, IAssetProvider assetProvider)
        {
            _staticDataService = staticDataService;
            _injectedAssetProvider = injectedAssetProvider;
            _assetProvider = assetProvider;
        }
        
        public UISorter CreateUICore()
        {
            if (_uiSorter != null) return _uiSorter;
            _uiSorter = _assetProvider.Instantiate(PrefabAddresses.UICore).GetComponent<UISorter>();
            return _uiSorter;
        }

        public GameOverWindow CreateGameOverWindow() => CreateWindow<GameOverWindow>(WindowID.GameOver);
        public MainMenuWindow CreateMainMenuWindow() => CreateWindow<MainMenuWindow>(WindowID.MainMenu);
        
        private T CreateWindow<T>(WindowID windowID) where T : WindowBase
        {
            if(_uiSorter == null) CreateUICore();
            GameObject window = _staticDataService.ForWindow(windowID);
            return _injectedAssetProvider.Instantiate(window, _uiSorter.WindowsParent).GetComponent<T>();
        }
    }
}