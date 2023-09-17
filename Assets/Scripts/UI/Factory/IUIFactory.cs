using MainGame.UI.GameWindows;

namespace MainGame.UI.Factory
{
    public interface IUIFactory
    {
        UISorter CreateUICore();
        GameOverWindow CreateGameOverWindow();
        MainMenuWindow CreateMainMenuWindow();
    }
}