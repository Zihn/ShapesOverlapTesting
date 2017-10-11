using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class dragErasers: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	GameObject startParent;
	RectTransform rectTrans;
	private Vector3 shiftPos;
	private Vector3 newPos;

	// Use this for initialization
	void Awake () {
		//Wait for the first frame
		Invoke("Initialize", 0.01f); ///on your start function to delay it a bit.    
	}

	void Initialize() {
		rectTrans = gameObject.GetComponent<RectTransform> ();
		startParent = GameObject.FindGameObjectWithTag ("Canvas");
		Rect parentRect = startParent.GetComponent<RectTransform> ().rect;
		shiftPos = new Vector3 (-parentRect.width / 2, -parentRect.height / 2, 0);
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
	}

	public void OnDrag (PointerEventData eventData)
	{			
		newPos = Input.mousePosition + shiftPos;
		//		newPos.z = 0;
		rectTrans.localPosition = newPos;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
	}
}