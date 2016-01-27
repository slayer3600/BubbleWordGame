using UnityEngine;
using System.Collections;

public class ConstantRotationScript : MonoBehaviour {

    private float rotationSpeed;

	// Use this for initialization
	void Start () {

        rotationSpeed = Random.Range(-100, 100);

    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
	
	}
}
