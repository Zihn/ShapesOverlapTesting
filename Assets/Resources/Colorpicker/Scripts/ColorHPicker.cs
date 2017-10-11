using UnityEngine;
using UnityEngine.EventSystems;

public class ColorHPicker : MonoBehaviour, IDragHandler
{
	public GameObject HBackground;
	public bool horizontalHueScale = true;
	private float width;
	private float height;
	private RectTransform rectTrans;
	private RectTransform bgTrans;
	private Vector3 position;
	private static float hueValue;

	// Use this for initialization
	void Awake () {
		//Wait for the first frame
		Invoke("Initialize", 0.02f); ///on your start function to delay it a bit.    
	}

//	void Update()
//	{
//		if (Input.GetMouseButtonDown (0)) {
//			// inrect is set to return always false.. This is for later
//			if (inRect(Input.mousePosition)) {
//				float hueValue = SetPickerPositionFromScreen (Input.mousePosition);
//				ColorSBPicker.SetHue (hueValue);
//			}
//		}
//	}

	void Initialize() {
		bgTrans = HBackground.GetComponent<RectTransform> ();
		rectTrans = gameObject.GetComponent<RectTransform> ();
		width = bgTrans.rect.width;
		height = bgTrans.rect.height;
		Vector2 p = rectTrans.anchoredPosition;
		ColorSBPicker.SetHue (GetHueValueFromPoint ((horizontalHueScale) ? p.x : p.y));
	}

	public void OnDrag (PointerEventData eventData)
	{
		float hueValue = SetPickerPositionFromScreen (Input.mousePosition);
		ColorSBPicker.SetHue (hueValue);
		ColorPicker.SetTargetColor ();
	}

	float SetPickerPositionFromScreen(Vector3 point)
	{
		Canvas myCanvas = GetComponent<Transform>().root.GetComponent<Canvas>();
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(bgTrans, Input.mousePosition, myCanvas.worldCamera, out pos);
		float newPosX = Mathf.Clamp (pos.x * width, -width / 2, width/2);
		float newPosY = Mathf.Clamp (pos.y * height, -height / 2, height/2);

		newPosX *= ( horizontalHueScale) ? 1f : 0f;
		newPosY *= ( !horizontalHueScale) ? 1f : 0f;
		rectTrans.localPosition = new Vector3 (newPosX, newPosY, 0f);

		return GetHueValueFromPoint((horizontalHueScale) ? newPosX : newPosY);
	}

	float GetHueValueFromPoint(float pos)
	{
		float max = (horizontalHueScale) ? width : height;
		pos += max / 2;
		hueValue = 1 - pos / max;
		return hueValue;
	}

	public static float GetCurrentHue()
	{
		return hueValue;
	}

	bool inRect(Vector3 pos) 
	{
		return false;
	}
}
