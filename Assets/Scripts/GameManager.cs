using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        //am.PlayMusic("track1");
        am.Initialize();
        StartCoroutine(am.playNextTrack());
        //am.playNextTrack();
    }

}
