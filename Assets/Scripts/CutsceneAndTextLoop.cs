using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CutsceneAndTextLoop : MonoBehaviour
{
    public float timeToShow;
    public TransmitCutscene transmitCutscene;
    public DetectBinObject detectBinObject;
    public Camera computerCamera;
    public VideoPlayer computerCutscenePlayer;
    public Text computerText;
    public string[] hints;

    bool showingCutscene;
    bool showFirstTypeVideo;
    float textTime;
	// Use this for initialization
	void Start ()
    {
        showingCutscene = true;
        textTime = 0;
        transmitCutscene.AddAfterLoopComputer(computerCutscenePlayer, new VideoPlayer.EventHandler(EndVideo));
        showFirstTypeVideo = true;
        computerText.text = hints[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(textTime > timeToShow)
        {
            showingCutscene = true;
        }

        if(showingCutscene)
        {
            showingCutscene = true;
            int keyItemCounter;
            if(showFirstTypeVideo)
            {
                keyItemCounter = detectBinObject.keyItemCounter;
            }
            else
            {
                keyItemCounter = detectBinObject.keyItemCounter + 3;
            }
            transmitCutscene.ActivateCutsceneComputer(computerCutscenePlayer, keyItemCounter, computerCamera);
        }
        else
        {
            textTime += Time.deltaTime;
        }

	}

    void EndVideo(VideoPlayer source)
    {
        source.enabled = false;
        showingCutscene = false;
        textTime = 0;
        computerText.text = hints[detectBinObject.keyItemCounter];
        if (detectBinObject.keyItemCounter != 0)
        {
            showFirstTypeVideo = !showFirstTypeVideo;
            print(showFirstTypeVideo);
        }
    }
}
