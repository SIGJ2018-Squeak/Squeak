using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squeaks: MonoBehaviour {
    public List<AudioClip> squeakSFX;

    public void PlaySqueak()
    {
        if (squeakSFX.Count > 0)
        {
            AudioSource.PlayClipAtPoint(squeakSFX[(int)Random.Range(0, squeakSFX.Count - 1)], transform.position, Random.Range(0.5f,1f));
        }
    }
}
