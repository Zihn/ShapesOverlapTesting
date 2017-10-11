using UnityEngine;

public class ColorPicker : MonoBehaviour 
{
	private static Shape target;
	private static Color selectedColor;

	public static Color GetColor()
	{
		Vector2 sb = ColorSBPicker.GetSBValues ();
		float h = ColorHPicker.GetCurrentHue ();

		return new HSBColor (h, sb.x, sb.y).ToColor ();
	}

	public static void SetTargetColor()
	{
		selectedColor = GetColor();
		if (target) {
			target.color = selectedColor;
		}
	}

	public static void SetTarget(GameObject obj)
	{
		target = obj.GetComponent<Shape> ();
	}

	public static void RemoveTarget()
	{
		target = null;
	}

}