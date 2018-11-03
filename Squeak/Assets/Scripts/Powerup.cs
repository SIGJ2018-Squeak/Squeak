using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    public float powerupCooldown = 1.0f;
    private bool powerupAvailable = true;

    

    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (powerupAvailable)
        {
            sound.Play();
            powerupAvailable = false;
            Invoke("ResetPowerup", powerupCooldown);

            Debug.Log("Picked Up " + gameObject.ToString());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
        if (powerupAvailable)
        {
            sound.Play();
            powerupAvailable = false;
            Invoke("ResetPowerup", powerupCooldown);

            Debug.Log("Stay Picked Up " + gameObject.ToString());
        }
    }

    private void ResetPowerup()
    {
        powerupAvailable = true;
    }

}
