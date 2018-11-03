using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartTimer : MonoBehaviour {

    public float restartDelay = 15.0f;


	// Use this for initialization
	void Start () {
        Invoke("GoToStart", restartDelay);
	}
	
    void GoToStart()
    {
        SceneLoader.GoToScene(0);
    }
}
