using UnityEngine;
using System.Collections;

public class ColorVowels : MonoBehaviour {

    public Color vowelColor;

	// Use this for initialization
	void Start () {

        TextMesh tm = GetComponent<TextMesh>();

        //tm.text = "Hi";
        tm.color = vowelColor;


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
