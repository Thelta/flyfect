using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;


public class TransmitCutscene : MonoBehaviour
{
	FirstPersonController fpsController;
	PickItem pickItem;
	SitComputer sitComputer;
	VideoPlayer.EventHandler cameraAfterLoop;
    int usedCutscene;

	public VideoClip[] cutsceneClips;

	void Awake ()
	{
		fpsController = GetComponent<FirstPersonController>();
		pickItem = GetComponentInChildren<PickItem>();
		sitComputer = GetComponentInChildren<SitComputer>();

		cameraAfterLoop = new VideoPlayer.EventHandler(EndCutscene);

	}

	public void AddAfterLoopExternal(VideoPlayer cutscenePlayer)
	{
		cutscenePlayer.loopPointReached += cameraAfterLoop;
        cutscenePlayer.loopPointReached += new VideoPlayer.EventHandler(ExitToFinalScene);
	}

	public void AddAfterLoopComputer(VideoPlayer cutscenePlayer, VideoPlayer.EventHandler computerAfterLoop)
	{
		cutscenePlayer.loopPointReached += computerAfterLoop;
	}

	public void ActivateCutsceneExternal(VideoPlayer cutscenePlayer, int cutsceneIndex, Camera camera)
	{
		cutscenePlayer.enabled = true;

		//disable user interactions
		fpsController.enabled = false;
		pickItem.enabled = false;
		sitComputer.enabled = false;

		//Change Camera
		cutscenePlayer.targetCamera = camera;

		//start cutscene
		cutscenePlayer.clip = cutsceneClips[cutsceneIndex];
		cutscenePlayer.Play();
	}

	public void ActivateCutsceneComputer(VideoPlayer cutscenePlayer, int cutsceneIndex, Camera camera)
	{
		cutscenePlayer.enabled = true;

		//Change Camera
		cutscenePlayer.targetCamera = camera;

		//start cutscene
		cutscenePlayer.clip = cutsceneClips[cutsceneIndex];
		cutscenePlayer.Play();

	}

	void EndCutscene(VideoPlayer source)
	{
		source.enabled = false;

		//enable user interaction
		fpsController.enabled = true;
		pickItem.enabled = true;
		sitComputer.enabled = true;
        usedCutscene++;
	}

    void ExitToFinalScene(VideoPlayer source)
    {
        if(usedCutscene == 4)
        {
            SceneManager.LoadScene(1);
        }
    }
}
