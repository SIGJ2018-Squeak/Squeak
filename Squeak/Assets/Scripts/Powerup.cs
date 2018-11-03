using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    public float powerupCooldown = 1.0f;
    private bool powerupAvailable = true;

    public enum PowerUpType { ReverseGravity };

    public PowerUpType type;

    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (powerupAvailable)
        {
           
            PowerUp();

            Debug.Log("Picked Up " + gameObject.ToString());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
        if (powerupAvailable)
        {
            PowerUp();

            Debug.Log("Stay Picked Up " + gameObject.ToString());
        }
    }

    private void ResetPowerup()
    {
        powerupAvailable = true;
    }

    private void PowerUp()
    {
        sound.Play();
        powerupAvailable = false;
        Invoke("ResetPowerup", powerupCooldown);

        switch (type)
        {
            case PowerUpType.ReverseGravity:
                {
                    PlayerMovement player = GameObject.FindObjectOfType<PlayerMovement>();

                    player.gravityReversed = !player.gravityReversed;
                    
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

}
