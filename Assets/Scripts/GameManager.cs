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
        StartCoroutine(am.PlayNextTrack());
        //playNext();
    }

    IEnumerator playNext()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(am.PlayNextTrack());
    }

}
