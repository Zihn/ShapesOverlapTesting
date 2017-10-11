using UnityEngine;
using System.Collections;

public class drawShapeOverlap : MonoBehaviour {

	public int TposX = 0;
	public int TposY = 0;
	public int Twidth = 100;
	public int Theight = 100;

//	bool check(GameObject a, GameObject b){
//		float aw = a.transform.localScale.x;
//		float ah = a.transform.localScale.y;
//		float bw = b.transform.localScale.x;
//		float bh = b.transform.localScale.y;
//
//		float ax = a.transform.position.x;
//		float ay = a.transform.position.y;
//		float bx = b.transform.position.x;
//		float by = b.transform.position.y;
//
//		float[] aXCoords = {ax, ax+aw};
//		float[] bXCoords = {bx, bx+bw};
//		float[] aYCoords = {ay, ay-ah};
//		float[] bYCoords = {ay, ay-ah};
//
//		float DX = Mathf.Min (aXCoords.Max(), bXCoords.Max()) - Mathf.Max(aXCoords.Min(), bXCoords.Min());
//		float DY = Mathf.Min (aYCoords.Max(), bYCoords.Max()) - Mathf.Max(aYCoords.Min(), bYCoords.Min());
//
//		if (DX >= 0 && DY >= 0) {
//			Vector2 sR = new Vector2 (Mathf.Min (aXCoords.Max(), bXCoords.Max()),  Mathf.Min (aYCoords.Max(), bYCoords.Max()));
//			Vector2 eR = new Vector2 (Mathf.Max (aXCoords.Min(), bXCoords.Min()),  Mathf.Max (aYCoords.Min(), bYCoords.Min()));
//			DrawOverlapRectOnTexture(sR, eR);
//			return true;
//		}
//
//		return false;
//	}

	void DrawOverlapRectOnTexture(Vector2 start, Vector2 end) 
	{
		Vector2[] vertices = new Vector2[4];
		float width = start.x + Mathf.Abs(end.x - start.x);
		float height = start.y + Mathf.Abs(end.y - start.y);
		vertices [0] = start;
		vertices [1] = new Vector2(start.x + width, end.y);
		vertices [2] = end;
		vertices [3] = new Vector2(start.x, end.y + height);

		Vector2 centre = new Vector2(start.x + width/2, start.y + height/2);

		//Draw it

	}
}
