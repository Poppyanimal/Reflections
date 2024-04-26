using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSFXPlacer : MonoBehaviour
{
    public Transform placeLocation;
    public SFXPlayThenDecay sfxPrefab;
    public List<AudioClip> baseFootsteps;
    public float vol = 1f;
    public int curFSIndex = 0;
    public void playFootstep()
    {
        playRegularFootstep();
        //TODO: change sound based on ground to allow water splashing when walking through water
    }

    void playRegularFootstep()
    {
        if(curFSIndex >= baseFootsteps.Count)
            curFSIndex = 0;

        SFXPlayThenDecay dec = Instantiate(sfxPrefab, placeLocation.position, placeLocation.rotation);
        if(baseFootsteps.Count <= 0)
            return;
        dec.initThenPlay(baseFootsteps[curFSIndex], vol);
        curFSIndex++;
    }
}
