using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class showErasers : MonoBehaviour, IPointerClickHandler {

	private bool shown = false;
	public static Color colour = new Color();
	public Color visibleColour = new Color ();
	public Color invisibleColour = new Color ();

	public GameObject showIcon;
	public GameObject hiddenIcon;

	void Awake() {
		showIcon.SetActive(shown);
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (shown) {
			shown = false;
			colour = invisibleColour;
		} else {
			shown = true;
			colour = visibleColour;
		}
		changeVisibility ();
		changeButtonApearance ();
	}

	void changeButtonApearance()
	{
		showIcon.SetActive(shown);
		hiddenIcon.SetActive(!shown);
	}

	void changeVisibility() 
	{	
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Eraser");

		foreach (GameObject obj in objects) {
			obj.GetComponent<Shape> ().color = colour;
		}
	}
}
