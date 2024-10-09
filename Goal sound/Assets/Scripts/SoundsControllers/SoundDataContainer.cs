using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoundsControllers
{
    public class SoundDataContainer
    {
        public static List<SoundData> SoundsData { get; private set; }

        public static void LoadSoundsData()
        {
            SoundsData = Resources.LoadAll<SoundData>("SoundsData")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}

