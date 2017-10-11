using UnityEngine;
using UnityEngine.EventSystems;

public class changeColor : MonoBehaviour, IPointerClickHandler {

	private bool selected = false;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (selected) {
			selected = false;
			ColorPicker.RemoveTarget ();
		} else {
			selected = true;
			ColorPicker.SetTarget (gameObject);
		}
	}
		
	// Update is called once per frame
//	void Update () {
//		gameObject.GetComponent<Shape>().color = ColorPicker.GetColor ();
//	}
}
