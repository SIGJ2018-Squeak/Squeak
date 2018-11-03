using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTimeout : MonoBehaviour {
    public float helpTimeout = 10.0f;

	// Use this for initialization
	void Start () {

        Invoke("GoToNextScene", helpTimeout);
    }

    void GoToNextScene()
    {
        SceneLoader.GoToNextScene();
    }

}
