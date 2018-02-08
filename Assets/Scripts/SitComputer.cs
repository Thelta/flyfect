using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SitComputer : MonoBehaviour
{
	public LayerMask computerLayer;
	public float maxDistance;
	public float maxSpeed;
	public float minDistance;

	PickItem pickItem;
	bool isSitting;
	bool didSit;

	Vector3 prevPlayerPos;
	Transform chairTransform;

	// Use this for initialization
	void Start ()
	{
		pickItem = GetComponent<PickItem>();
		isSitting = false;
		didSit = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.E) && !isSitting)
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, computerLayer))
			{
				if(!didSit)
				{
					chairTransform = hit.transform.GetChild(0);
					Sit();
				}
				else
				{
					Stand();
				}
			}
		}

		if (isSitting)
		{
			Sit();
		}
	}

	void Sit()
	{
		if(!isSitting)
		{
			isSitting = true;
			prevPlayerPos = transform.position;
		}
		Vector3 chairPos = chairTransform.position;

		Vector3 vel = new Vector3();
		transform.position = (Vector3.SmoothDamp(transform.position, chairPos, ref vel, maxSpeed * Time.deltaTime));

		if(Vector3.Distance(transform.position, chairPos) < minDistance)
		{
			GetComponentInParent<FirstPersonController>().enabled = false;
			transform.LookAt(chairTransform.parent);
			isSitting = false;
			didSit = true;
		}
	}

	void Stand()
	{
		didSit = false;
		transform.position = prevPlayerPos;
		GetComponentInParent<FirstPersonController>().enabled = true;
	}
}
