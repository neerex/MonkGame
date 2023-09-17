using UnityEngine;

namespace MainGame.UI
{
    public class UISorter : MonoBehaviour
    {
        [field: SerializeField] public Transform WindowsParent { get; private set; }
        [field: SerializeField] public Transform PopupsParent  { get; private set; }
        
        //you can implement adding UI elements here to whichever layer you need to and extend on it
    }
}