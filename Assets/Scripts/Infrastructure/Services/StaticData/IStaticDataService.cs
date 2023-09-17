using MainGame.ScriptableConfigs;
using UnityEngine;

namespace MainGame.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void Initialize();
        GameObject ForWindow(WindowID windowID);
        void LoadWindowsData();
    }
}