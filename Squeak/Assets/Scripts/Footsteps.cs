using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {
    public List<AudioClip> footstepSFX;

    public void PlayFootstep()
    {
        if (footstepSFX.Count > 0)
        {
            AudioSource.PlayClipAtPoint(footstepSFX[(int)Random.Range(0, footstepSFX.Count - 1)], transform.position);
        }
    }
}
