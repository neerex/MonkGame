using System.Collections.Generic;
using System.Linq;
using MainGame.ScriptableConfigs;
using MainGame.StaticData;
using UnityEngine;

namespace MainGame.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowID, GameObject> _windows;
    
        public void Initialize()
        {
            LoadWindowsData();
        }
    
        public GameObject ForWindow(WindowID windowID)
        {
            return _windows.TryGetValue(windowID, out var prefab) ? prefab : null;
        }
    
        public void LoadWindowsData()
        {
            var data = Resources.LoadAll<WindowConfigSO>(ConfigAddresses.UIWindowsConfig);
            _windows = data.ToDictionary(x => x.Id, x => x.WindowPrefab);
        }
    }
}