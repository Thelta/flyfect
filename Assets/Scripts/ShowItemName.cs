using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemName : MonoBehaviour
{
	public float maxAngle;
	public float maxDistance;
	public Vector3 posOffset;
	public Transform playerTransform;


	Text itemText;
	Transform itemTransform;
	bool onFocus;
	MeshRenderer itemMR;


	void Start()
	{
		itemText = GetComponent<Text>();
		itemText.text = "";
		onFocus = false;
	}

	void LateUpdate()
	{
		if (onFocus && itemMR != null && itemMR.enabled) {
			Vector3 itemForward = itemTransform.position - playerTransform.position; //itemforward according to player
			float angle = Vector3.Angle (playerTransform.forward, itemForward);
			float distance = Vector3.Distance (itemTransform.position, playerTransform.position);
			Rigidbody itemRB = itemTransform.gameObject.GetComponent<Rigidbody> ();

			if (angle < maxAngle && distance < maxDistance) {
				SetTextRotation (itemForward);
			} else {
				onFocus = false;
				itemText.text = "";
			}

			if (itemRB.velocity.magnitude > 0) {
				SetTextPosition (itemTransform.position + posOffset);
			}
		} 
		else if(itemMR == null || !itemMR.enabled)
		{
			onFocus = false;
			itemText.text = "";
		}
	}

	public void AlignItemText(RaycastHit hit, Transform playerTransform)
	{

		itemText.text = hit.collider.name;
		itemTransform = hit.collider.transform;
		SetTextPosition(itemTransform.position + posOffset);
		SetTextRotation(itemTransform.position - playerTransform.position);
		onFocus = true;
		itemMR = hit.collider.GetComponent<MeshRenderer> ();
	}

	void SetTextPosition(Vector3 pos)
	{
		itemText.rectTransform.position = pos;
	}

	void SetTextRotation(Vector3 rot)
	{
		itemText.rectTransform.localRotation = Quaternion.LookRotation(rot);
	}

}
