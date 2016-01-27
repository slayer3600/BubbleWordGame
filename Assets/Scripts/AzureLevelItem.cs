using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [Serializable]
    public class AzureLevelItem
    {
        public int LevelID = 0;
        public int LevelNumber = 0;
        public string Title = "New Title";
        public string Description = "New Description";
        public string WinTitle = "New Win Title";
        public string WinDescription = "New Win Description";
        public float TimeLimit = 600;

        public int ThreeLetterWordCount = 0;
        public int FourLetterWordCount = 0;
        public int FiveLetterWordCount = 0;
        public int SixLetterWordCount = 0;
        public int SevenLetterWordCount = 0;
        public int EightLetterWordCount = 0;
        public int NineLetterWordCount = 0;
        public int TenLetterWordCount = 0;

        public string[] specificWordsRequired;
    }
}
