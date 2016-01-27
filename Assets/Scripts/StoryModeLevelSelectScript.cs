using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryModeLevelSelectScript : MonoBehaviour {

    public Canvas myCanvas;
    public GameObject levelSelectButton;
    public LevelItemList levelData;
    public int columns = 3;
    public float columnWidth = 100f;
    public float columnHeight = -100f;

    private Vector2 startingPoint;
    private int highestLevelAchieved;

    // Use this for initialization
    void Start () {

        highestLevelAchieved = PlayerPrefs.GetInt("HighestLevelAchieved", 0);
        CreateLevelSelectButtons();

    }
	
	void CreateLevelSelectButtons()
    {

        startingPoint = new Vector2(-100, -50);
        int i = 0;

        foreach (var level in levelData.level)
        {

            GameObject go;
            startingPoint += new Vector2(columnWidth, 0);
            go = (GameObject)Instantiate(levelSelectButton, startingPoint, Quaternion.identity);
            go.transform.SetParent(myCanvas.transform, false);

            if (level.Level > highestLevelAchieved + 1)
            {
                go.GetComponent<Button>().interactable = false;
            }

            go.GetComponentInChildren<Text>().text = level.Level.ToString();
            i++;

            if (i == columns)
            {
                //new column
                startingPoint += new Vector2(-startingPoint.x - columnWidth, columnHeight);
                i = 0;
            }

        }



    }
}
