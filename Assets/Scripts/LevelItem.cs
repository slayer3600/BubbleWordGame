[System.Serializable]
public class LevelItem {

    public int Level = 0;
    public string Title = "New Title";
    public string Description = "New Description";
    public string WinTitle = "New Win Title";
    public string WinDescription = "New Win Description";
    public float TimeLimit = 600;

    public int numOfThreeLetterWordsReq = 0;
    public int numOfFourLetterWordsReq = 0;
    public int numOfFiveLetterWordsReq = 0;
    public int numOfSixLetterWordsReq = 0;
    public int numOfSevenLetterWordsReq = 0;
    public int numOfEightLetterWordsReq = 0;
    public int numOfNineLetterWordsReq = 0;
    public int numOfTenLetterWordsReq = 0;

    public string[] specificWordsRequired;

}
