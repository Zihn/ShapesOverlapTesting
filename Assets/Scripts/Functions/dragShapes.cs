using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class dragShapes: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	GameObject startParent;
	RectTransform rectTrans;
	private Vector3 shiftPos;
	private Vector3 newPos;
	GameObject[] otherShapeObjects;
	ColorizeObjects CO;
	ShapeCreator SC;

	// Use this for initialization
	void Awake () {
		//Wait for the first frame
		Invoke("Initialize", 0.01f); ///on your start function to delay it a bit.    
	}

	void Initialize() {
		GameObject Controller = GameObject.FindGameObjectWithTag ("Magic");
		CO = Controller.GetComponent<ColorizeObjects> ();
		SC = Controller.GetComponent<ShapeCreator> ();
		rectTrans = gameObject.GetComponent<RectTransform> ();
		startParent = GameObject.FindGameObjectWithTag ("Canvas");

		Rect parentRect = startParent.GetComponent<RectTransform> ().rect;

		shiftPos = new Vector3 (-parentRect.width / 2, -parentRect.height / 2, 0);
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		otherShapeObjects = GameObject.FindGameObjectsWithTag ("shape");
	}

	public void OnDrag (PointerEventData eventData)
	{	
		//src:http://answers.unity3d.com/questions/849117/46-ui-image-follow-mouse-position.html
		// TODO Maybe use this in a later stage (it is more independant)
		//		Canvas myCanvas = GetComponent<Transform>().root.GetComponent<Canvas>();
		//		Vector2 pos;
		//		RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
		//		rectTrans.position = myCanvas.transform.TransformPoint(pos);

		newPos = Input.mousePosition + shiftPos;
//		newPos.z = 0;
		rectTrans.localPosition = newPos;
		SC.DestroyDebug ();
		checkNeighbors ();
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		otherShapeObjects = null;
	}

	void checkNeighbors()
	{	
		foreach (GameObject obj in otherShapeObjects) 
		{
			if (obj != gameObject) {
				float dist = Vector3.Distance (rectTrans.localPosition, obj.GetComponent<RectTransform> ().localPosition);
				if (dist < 2 * rectTrans.rect.width) {
					float t = (2 * rectTrans.rect.width) / (dist+0.01f) * 5;
//					Vector3 pos = obj.GetComponent<RectTransform> ().localPosition;
					Shape shape = obj.GetComponent<Shape> ();
					Vector3[] overLapVs = gameObject.GetComponent<Shape> ().OverlapVertices (shape);

					foreach (Vector3 v in overLapVs) {
						SC.DrawDebugCircle (v, t);
					}

//					Vector2[] veees = overLapVs;
//					DrawPolygon (overLapVs);
//					drawLine (newPos, obj.GetComponent<RectTransform>().localPosition, t);
//					drawCircle (newPos, t*2);
//					drawCircle (pos, t*2);

//					Vector3[] vees = obj.GetComponent<Shape> ().getVertices();
//					foreach (Vector3 v in vees) {
////						drawCircle (v+pos, t);
//						counter++;
//						bool inside = gameObject.GetComponent<Shape> ().IsInShape(v+pos);
//						if (inside) {
//							drawCircle (v+pos, t);
//						}
////						Debug.Log (gameObject.GetComponent<Shape> ().IsInShape(v+pos));
//					}


//					CO.ColorizeShape (gameObject.name);
//					CO.ColorizeShape (obj.name);
				}
			}
		}
	}

	void DrawPolygon(Vector3[] vs)
	{
		GameObject pol = GameObject.FindGameObjectWithTag ("polygon");
		Polygon P = pol.GetComponent<Polygon> ();
		P.DrawPolygon (vs);
	}
}