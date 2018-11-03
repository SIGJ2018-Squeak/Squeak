using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    public GameObject audioManager;

    void Awake () {
        if (GameObject.FindObjectOfType<AudioManager>() == null)
        {
            GameObject.Instantiate(audioManager);
        }
	}
	
	
}
