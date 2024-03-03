using System;
using System.Collections.Generic;

namespace ArchiveData
{
    [Serializable]
    public class Archive
    {
        public int min = 0;
        public int max = 0;

        public int anitype = 3;

        public int repetitionType = 0;

        public int language;//In LocalLanguugae_Manager
        public bool autoCheckUpdate = true;

        public float BGMVolume = 1f;

        public string XCAPI;

    }

}