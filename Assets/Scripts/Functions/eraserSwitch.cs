using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class eraserSwitch : MonoBehaviour, IPointerClickHandler {

	private bool switched = false;

	public void OnPointerClick(PointerEventData eventData) {
		spawnShapes.Switch ();

		Circle C = gameObject.GetComponent<Circle> ();
		if (switched) {
			switched = false;
			C.fill = true;
		} else {
			switched = true;
			C.fill = false;
 		}

		C.OnRebuildRequested ();
	}
}
