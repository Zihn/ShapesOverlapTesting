using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class spawnShapes : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	private int shapeNR;
	private GameObject Controller;
	private ColorizeObjects CO;
	private ShapeCreator SC;
	private Vector3 newPos;
	private Vector3 shiftPos;
	private static bool spawnErasers = false;
	GameObject startParent;
	GameObject itemBeingDragged;
	Vector3 startPosition;
	RectTransform rectTrans;
	GameObject[] objects;

	// Use this for initialization
	void Awake () {
		//Wait for the first frame
		Invoke("Initialize", 0.01f); ///on your start function to delay it a bit.    
	}

	void Initialize()
	{
		Controller = GameObject.FindGameObjectWithTag ("Magic");
		CO = Controller.GetComponent<ColorizeObjects> ();
		SC = Controller.GetComponent<ShapeCreator> ();

		rectTrans = gameObject.GetComponent<RectTransform> ();
		startPosition = rectTrans.localPosition;

		startParent = GameObject.FindGameObjectWithTag ("Canvas");
		Rect parentRect = startParent.GetComponent<RectTransform> ().rect;

		shiftPos = new Vector3 (-parentRect.width / 2, -parentRect.height / 2, 0);

		if (gameObject.name == "Circle") {
			shapeNR = 0;
		}else if (gameObject.name == "Triangle") {
			shapeNR = 1;
		}else{
			shapeNR = 2;
		}	
	}

	void spawnShape(float x, float y) 
	{
		if (spawnErasers) {
			int ID = SC.CreateEraser (shapeNR, x, y);
			string name = "shape" + ID;
		} else {
			int ID = SC.CreateShape (shapeNR, x, y);
			string name = "shape" + ID;
			CO.ColorizeShape (name);
		}
	}

	public static void Switch() 
	{
		if (spawnErasers) {
			spawnErasers = false;
		} else {
			spawnErasers = true;
		}
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		objects = GameObject.FindGameObjectsWithTag ("shape");
	}

	public void OnDrag (PointerEventData eventData)
	{			
		newPos = Input.mousePosition + shiftPos;
		//		newPos.z = 0;
		rectTrans.localPosition = newPos;
		checkNeighbors ();
	}

	public void OnEndDrag (PointerEventData eventData)
	{
//		newPos = Input.mousePosition + shiftPos;
		spawnShape (newPos.x, newPos.y);
		rectTrans.localPosition = startPosition;
		objects = null;
	}

	void checkNeighbors()
	{
		foreach (GameObject obj in objects) 
		{
			float dist = Vector3.Distance (rectTrans.localPosition, obj.GetComponent<RectTransform> ().localPosition);
		}
	}
}