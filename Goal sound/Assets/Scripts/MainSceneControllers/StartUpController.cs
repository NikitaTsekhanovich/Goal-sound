using SoundsControllers;
using UnityEngine;

namespace MainMenuControllers
{
    public class StartUpController : MonoBehaviour
    {
        private void Awake()
        {
            SoundDataContainer.LoadSoundsData();
        }
    }
}

