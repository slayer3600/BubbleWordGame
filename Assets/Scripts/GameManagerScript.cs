using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public GameObject bubblePrefab;
    public int maxNoBubbles = 10;
    public Transform[] spawnPoints;
    public TextAsset dictionary;
    public int minimumNumberOfLetters = 2;
    public Text scoreTextUI;
    public Text wordTextUI;
    public Text timerTextUI;
    public Text levelTitleUI;
    public Text levelDescriptionUI;
    public Text foundWordlist;
    public AudioClip invalidWordSound;
    public AudioClip validWordSound;
    public AudioClip bigWordSound;
    public AudioClip levelWonSound;
    public AudioClip levelLostSound;
    public float timerSeconds = 0;
    public float bubbleSpawnInterval = .5f;
    public GameObject startDialog;
    public Button submitButton;
    public Button continueButton;
    public Button okButton;
    public Button quitButton;

    private AudioSource source;
    private int currentNoBubbles;
    private string[] wordList;
    private List<string> wordsFound = new List<string>();
    private string currentWord = string.Empty;
    private LevelItem levelData;
    //private AzureLevelItem levelData;
    private bool levelStarted = false;

    private int numOfThreeLetterWordsReq = 0;
    private int numOfFourLetterWordsReq = 0;
    private int numOfFiveLetterWordsReq = 0;
    private int numOfSixLetterWordsReq = 0;
    private int numOfSevenLetterWordsReq = 0;
    private int numOfEightLetterWordsReq = 0;
    private int numOfNineLetterWordsReq = 0;
    private int numOfTenLetterWordsReq = 0;

    void Awake()
    {
        
        Time.timeScale = 0f;
        submitButton.enabled = false;
        continueButton.gameObject.SetActive(false);
        SetupLevelConfigData();
    }

    // Use this for initialization
    void Start () {
     
    }

    void StartGame()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        submitButton.enabled = true;
        
        timerSeconds = levelData.TimeLimit + 1;

        CreateDictionary();
        source = GetComponent<AudioSource>();
        InvokeRepeating("SpawnBubbles", 0f, bubbleSpawnInterval);

    }

    void SetupLevelConfigData()
    {
        levelData = GameMasterScript.masterScript.GetCurrentLevelData();

        levelTitleUI.text = levelData.Title.Replace("/n", System.Environment.NewLine);
        levelDescriptionUI.text = levelData.Description.Replace("/n", System.Environment.NewLine);
        numOfThreeLetterWordsReq = levelData.ThreeLetterWordCount;
        numOfFourLetterWordsReq = levelData.FourLetterWordCount;
        numOfFiveLetterWordsReq = levelData.FiveLetterWordCount;
        numOfSixLetterWordsReq = levelData.SixLetterWordCount;
        numOfSevenLetterWordsReq = levelData.SevenLetterWordCount;
        numOfEightLetterWordsReq = levelData.EightLetterWordCount;
        numOfNineLetterWordsReq = levelData.NineLetterWordCount;
        numOfTenLetterWordsReq = levelData.TenLetterWordCount;

    }

    public void Resume()
    {
        startDialog.gameObject.SetActive(false);
        Time.timeScale = 1f;
        StartGame();
        levelStarted = true;
    }

    public void LoadTitleScreen()
    {
        //Application.LoadLevel("title");
        SceneManager.LoadScene("title");
    }

    public void LoadLevelSelectScreen()
    {
        //Application.LoadLevel("storymode");
        SceneManager.LoadScene("storymode");
    }

    // Update is called once per frame
    void Update () {

        if (levelStarted)
        {
            UpdateTimer();  
        }
        
    }

    void UpdateTimer()
    {

        if (timerSeconds < 1)
        {
            LevelLost();
            return;
        }

        timerSeconds -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timerSeconds / 60F);
        int seconds = Mathf.FloorToInt(timerSeconds - minutes * 60);

        string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerTextUI.text = niceTime;


    }

    private void SpawnBubbles()
    {

        currentNoBubbles = GameObject.FindGameObjectsWithTag("Bubble").Length;

        if (currentNoBubbles < maxNoBubbles)
        {
            Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            Instantiate(bubblePrefab, spawnPoint, Quaternion.identity);
        }

    }

    public void BuildWord(string letter)
    {
        currentWord += letter;
        wordTextUI.text = currentWord;
    }

    public void UnbuildWord(string letter)
    {

        int index = currentWord.LastIndexOf(letter);
        currentWord = currentWord.Remove(index, 1);
        wordTextUI.text = currentWord;

        
    }

    void CheckRules()
    {

        int threeLetterWordsFound = 0;
        int fourLetterWordsFound = 0;
        int fiveLetterWordsFound = 0;
        int sixLetterWordsFound = 0;
        int sevenLetterWordsFound = 0;
        int eightLetterWordsFound = 0;
        int nineLetterWordsFound = 0;
        int tenLetterWordsFound = 0;

        foreach (var word in wordsFound)
        {

            switch (word.Length)
            {
                case 3:
                    threeLetterWordsFound += 1;
                    break;
                case 4:
                    fourLetterWordsFound += 1;
                    break;
                case 5:
                    fiveLetterWordsFound += 1;
                    break;
                case 6:
                    sixLetterWordsFound += 1;
                    break;
                case 7:
                    sevenLetterWordsFound += 1;
                    break;
                case 8:
                    eightLetterWordsFound += 1;
                    break;
                case 9:
                    nineLetterWordsFound += 1;
                    break;
                case 10:
                    tenLetterWordsFound += 1;
                    break;
            }

        }

        if (threeLetterWordsFound >= numOfThreeLetterWordsReq && 
            fourLetterWordsFound >= numOfFourLetterWordsReq &&
            fiveLetterWordsFound >= numOfFiveLetterWordsReq &&
            sixLetterWordsFound >= numOfSixLetterWordsReq &&
            sevenLetterWordsFound >= numOfSevenLetterWordsReq &&
            eightLetterWordsFound >= numOfEightLetterWordsReq &&
            nineLetterWordsFound >= numOfNineLetterWordsReq &&
            tenLetterWordsFound >= numOfTenLetterWordsReq)
        {

            //all rules passed, user has won
            LevelWon();

        }
    }

    void StopPlay()
    {
        //Time.timeScale = 0; 
        CancelInvoke();
        levelStarted = false;
        submitButton.enabled = false;
        PopAllBubbles();

        continueButton.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);
    }

    void LevelLost()
    {
        StopPlay();
        DisplayFoundWords();
        levelTitleUI.text = "Try Again";
        levelDescriptionUI.text = "No worries, I've bet you've done worse things before. Dust it off and try again!";
        startDialog.gameObject.SetActive(true);
        source.PlayOneShot(levelLostSound);
    }

    void LevelWon()
    {

        StopPlay();
        DisplayFoundWords();
        levelTitleUI.text = levelData.WinTitle.Replace("/n", System.Environment.NewLine);
        levelDescriptionUI.text = levelData.WinDescription.Replace("/n", System.Environment.NewLine);
        startDialog.gameObject.SetActive(true);
        source.PlayOneShot(levelWonSound);

        int highestLevelAchieved = PlayerPrefs.GetInt("HighestLevelAchieved", 0);

        if (levelData.LevelNumber > highestLevelAchieved)
        {
            PlayerPrefs.SetInt("HighestLevelAchieved", levelData.LevelNumber);
        }

    }

    public void DisplayFoundWords()
    {

        StringBuilder wordlist = new StringBuilder();

        foreach (var word in wordsFound)
        {
            wordlist.AppendLine("- " + word);
        }

        foundWordlist.text = wordlist.ToString();
    }

    public void CreateWord()
    {

        if (IsValidWord(currentWord))
        {
            source.PlayOneShot(validWordSound);

            if (currentWord.Length > 3)
            {
                source.PlayOneShot(bigWordSound);
            }

            wordsFound.Add(currentWord);
            scoreTextUI.text = wordsFound.Count.ToString();
            PopUsedBubbles();
            CheckRules();

        }
        else
        {
            source.PlayOneShot(invalidWordSound);
            ResetBubbles();
        }

        currentWord = string.Empty;
        wordTextUI.text = string.Empty;
    }


    bool IsValidWord(string word)
    {
        int pos = System.Array.IndexOf(wordList, word.ToLower());

        if (pos > 1 && word.Length > minimumNumberOfLetters)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CreateDictionary()
    {

        char[] delimeters = new char[] { '\r', '\n' };

        //remove apostrophe's and convert to lower case
        string modifiedDictionary = dictionary.text.Replace("'", "").ToLower();
        wordList = modifiedDictionary.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);

    }

    void PopUsedBubbles()
    {

        GameObject[] Bubbles = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject bubble in Bubbles)
        {
            BubbleScript bs = bubble.GetComponent<BubbleScript>();

            if (bs.isSelected)
            {
                bs.Pop();
            }
        }

    }

    void PopAllBubbles()
    {

        GameObject[] Bubbles = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject bubble in Bubbles)
        {
            BubbleScript bs = bubble.GetComponent<BubbleScript>();
            if (!bs.isSelected)
            {
                bs.Pop();
            }
        }

    }

    void ResetBubbles()
    {
        GameObject[] Bubbles = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject bubble in Bubbles)
        {
            BubbleScript bs = bubble.GetComponent<BubbleScript>();

            if (bs.isSelected)
            {
                bs.Reset();
            }
        }
    }
}
