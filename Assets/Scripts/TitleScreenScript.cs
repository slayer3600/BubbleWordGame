using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour {

    public void LoadLevel(string level)
    {
        //PlayerPrefs.DeleteAll();
        Vibration.Vibrate(25);
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    }


}
