using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
	private Vector3 mOffset;
	private float mZCoord;

	void OnMouseDown()
	{
		mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		mOffset = gameObject.transform.position - GetMouseWorldPos();
	}

	private Vector3 GetMouseWorldPos()
	{
		var mousePoint = Input.mousePosition;
		mousePoint.z = mZCoord;

		return Camera.main.ScreenToWorldPoint(mousePoint);
	}

	private void OnMouseDrag()
	{
		var newPosition = GetMouseWorldPos() + mOffset;
		newPosition.y = newPosition.y + 0.2f;
		transform.position = newPosition;
	}

	private void OnMouseUp()
	{
		//Debug.Log("Validate if assembled correctly!");
	}
}
