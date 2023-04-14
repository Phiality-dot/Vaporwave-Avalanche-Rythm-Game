using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dndScript : MonoBehaviour
{
    public GameObject cMusic;
    private AudioSource cMusicAS;
    public randomobjectspawner masterspawner;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        masterspawner = GameObject.Find("masterspawner").GetComponent<randomobjectspawner>();
        cMusicAS = cMusic.GetComponent<AudioSource>();
        if(LevelInfo.clipidyclip != null && LevelInfo.beatTresh > -1f && LevelInfo.secFromStart > -1f)
        {
            masterspawner.beatThreshold = LevelInfo.beatTresh;
            cMusicAS.clip = LevelInfo.clipidyclip;
            cMusicAS.time = LevelInfo.secFromStart;
            cMusicAS.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
