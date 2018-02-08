using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickItem : MonoBehaviour
{
	public LayerMask itemLayerMask;
	public float itemPickDistance;
	public Transform grabbedItemPlace;
	public ShowItemName showItemName;

	GameObject pickedItem;
	Rigidbody itemRB;
	bool isPickedAnItem;

	void Start()
	{
		isPickedAnItem = false;
	}

	// Update is called once per frame
	void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, itemPickDistance, itemLayerMask))
		{
			showItemName.AlignItemText(hit, transform);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (isPickedAnItem)
			{
				DropItem();
			}
			else
			{
				GrabItem(hit);
			}
		}

		if (isPickedAnItem && (itemRB.velocity.magnitude > 0 || itemRB.angularVelocity.magnitude > 0)) 
		{
			itemRB.velocity = new Vector3 (0, 0, 0);
			itemRB.angularVelocity = new Vector3 (0, 0, 0);
		}

	}

	void GrabItem(RaycastHit hit)
	{

		if (hit.collider != null)
		{
			pickedItem = hit.collider.gameObject;
			pickedItem.transform.SetParent(grabbedItemPlace);
			pickedItem.transform.localPosition = new Vector3(0, 0, 0);
			pickedItem.transform.localRotation = Quaternion.identity;
			itemRB = pickedItem.GetComponent<Rigidbody>();
			itemRB.useGravity = false;
			itemRB.velocity = new Vector3(0, 0, 0);
			itemRB.angularVelocity = new Vector3(0, 0, 0);
			isPickedAnItem = true;
		}
	}

	void DropItem()
	{
		isPickedAnItem = false;
		pickedItem.GetComponent<Rigidbody>().useGravity = true;
		pickedItem.transform.SetParent(null);
		pickedItem = null;
	}




}
