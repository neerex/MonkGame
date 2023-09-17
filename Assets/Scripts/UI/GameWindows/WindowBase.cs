using UnityEngine;

namespace MainGame.UI.GameWindows
{
    public class WindowBase : MonoBehaviour
    {
        protected void CloseWindow() => Destroy(gameObject);
    }
}