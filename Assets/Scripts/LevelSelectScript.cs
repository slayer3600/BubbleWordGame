using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

    void Start()
    {

        Button btnLevel = GetComponent<Button>();
        
        btnLevel.onClick.RemoveAllListeners();
        btnLevel.onClick.AddListener(LoadLevel);

    }

    void LoadLevel()
    {
        Vibration.Vibrate(25);
        int selectedLevel = int.Parse(GetComponentInChildren<Text>().text);
        GameMasterScript.masterScript.SetLevel(selectedLevel);
        //Application.LoadLevel("level1");
        SceneManager.LoadScene("level1");
    }

}
