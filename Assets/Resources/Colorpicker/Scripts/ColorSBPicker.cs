using UnityEngine;
using UnityEngine.EventSystems;

public class ColorSBPicker : MonoBehaviour, IDragHandler 
{
	public GameObject sbBackground;
	private static Material sbMaterial;
	private static float width;
	private static float height;
	private RectTransform rectTrans;
	private RectTransform bgTrans;
	private static Vector2 pickerPosition;

	// Use this for initialization
	void Awake () {
		//Wait for the first frame
		Invoke("Initialize", 0.01f); ///on your start function to delay it a bit.    
	}

//	void Update()
//	{
//		if (Input.GetMouseButtonDown (0)) {
//			// inrect is set to return always false.. This is for later
//			if (inRect (Input.mousePosition)) {
//				pickerPosition = SetPickerPositionFromScreen (Input.mousePosition);
//			}
//		}
//	}

	void Initialize() {
		bgTrans = sbBackground.GetComponent<RectTransform> ();
		sbMaterial = sbBackground.GetComponent<MeshRenderer> ().material;
		rectTrans = gameObject.GetComponent<RectTransform> ();
		width = bgTrans.rect.width;
		height = bgTrans.rect.height;
	}
		
	public void OnDrag (PointerEventData eventData)
	{
		pickerPosition = SetPickerPositionFromScreen (Input.mousePosition);
		ColorPicker.SetTargetColor ();
	}

	Vector2 SetPickerPositionFromScreen(Vector3 point)
	{
		Canvas myCanvas = GetComponent<Transform>().root.GetComponent<Canvas>();
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(bgTrans, point, myCanvas.worldCamera, out pos);
		float newPosX = Mathf.Clamp (pos.x * width, -width / 2, width/2);
		float newPosY = Mathf.Clamp (pos.y * height, -height / 2, height/2);
		rectTrans.localPosition = new Vector3 (newPosX, newPosY, 0f);
		return new Vector2 (newPosX, newPosY);
	}

	public static void SetHue(float hue)
	{
		sbMaterial.color = new HSBColor(hue, 1, 1).ToColor();
	}

	public static Vector2 GetSBValues()
	{
		float s = 1 - (pickerPosition.x + width / 2)/width;
		float b = 1 - (pickerPosition.y + height / 2)/height;
		return new Vector2(s, b);
	}

	bool inRect(Vector3 pos) 
	{
		return false;
	}
}