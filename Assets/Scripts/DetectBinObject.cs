using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DetectBinObject : MonoBehaviour
{
	public GameObject boomParticlePrefab;
	public GameObject congrulateParticlePrefab;
	public LayerMask itemLayer;
	public string keyItemTag;
	public string mcTag;
	public string[] keyItemNames;
	public Camera FPSCamera;
	public VideoPlayer cutscene;

	[HideInInspector]
	public int keyItemCounter = 0;

	TransmitCutscene tc;

	void Start()
	{
		GameObject mc = GameObject.FindGameObjectWithTag(mcTag);
		tc = mc.GetComponent<TransmitCutscene>();
		tc.AddAfterLoopExternal(cutscene);
	}

	void OnTriggerEnter(Collider other)
	{

		if(((1 << other.gameObject.layer) & itemLayer) != 0)
		{
			if(!other.CompareTag(keyItemTag))
			{
				SetParticlePrefab(other, boomParticlePrefab);
				other.GetComponent<MeshRenderer>().enabled = false;
			}
			else if(other.name == keyItemNames[keyItemCounter]) 
			{
				keyItemCounter++;
				SetParticlePrefab(other, congrulateParticlePrefab);
				other.GetComponent<MeshRenderer>().enabled = false;

				GameObject mc = GameObject.FindGameObjectWithTag(mcTag);
				mc.GetComponent<TransmitCutscene>().ActivateCutsceneExternal(cutscene, keyItemCounter + 3, FPSCamera);
			}
		}
	}

	void SetParticlePrefab(Collider other, GameObject particlePrefab)
	{
		GameObject particleCopy = Instantiate(particlePrefab, new Vector3(0, 0, 0), Quaternion.identity);
		particleCopy.transform.SetParent(other.gameObject.transform);
		particleCopy.transform.localPosition = new Vector3 (0, 0, 0);
		ParticleSystem boom = particleCopy.GetComponent<ParticleSystem>();

		boom.Play();
		//Destroy(other.gameObject, boom.main.startLifetime.constantMax);
	}
}
