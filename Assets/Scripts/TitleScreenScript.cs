using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour {

    public void LoadLevel(string gameMode)
    {
        //PlayerPrefs.DeleteAll();
        //Vibration.Vibrate(25);
        //Application.LoadLevel(level);
        GameMasterScript.masterScript.SetGameMode(gameMode);
        //SceneManager.LoadScene("storymode");
        SceneManager.LoadScene(gameMode);
    }


}
