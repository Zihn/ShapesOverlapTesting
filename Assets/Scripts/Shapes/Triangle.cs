using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Triangle : Shape
{
	protected override void OnPopulateMesh(Mesh vh)
	{
		vh.Clear ();
		VertexHelper vbo = new VertexHelper(vh);
		// This only works with anchor on center mid
		float hW = rectTransform.rect.width / 2;
		float hH = rectTransform.rect.height / 2;

		Vector2 uv0 = new Vector2(0, 0);
		Vector2 uv1 = new Vector2(0, 1);
		Vector2 uv2 = new Vector2(1, 1);
		Vector2 uv3 = new Vector2(1, 0);
		Vector2 pos0;Vector2 pos1;Vector2 pos2;Vector2 pos3;
		// The triangle side lengths are based on the recttransform width
		// The radius of triangle is 2/3 of the triangle height. 
		float tHr = Mathf.Sqrt ((2 * hH * 2 * hH) - (hH * hH))/3; 
		float ofs = (2 * hH) - tHr*3;  //offset to put the triangle in the middle of the rectT
		plus = 0;
		if (fill) {
			shapeVerts = new Vector3[4];
			pos0 = new Vector2 (-hW, -tHr-ofs);
			pos1 = new Vector2 (hW, -tHr-ofs);
			pos2 = new Vector2 (0, tHr*2-ofs);
			pos3 = pos0;
			vbo.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
		} else {
			Vector2 prevO = new Vector2 (-hW, -tHr-ofs);
			Vector2 prevI = new Vector2 (-hW, -tHr-ofs);
			float degrees = 120f;
			int vertices = 3 + 1;  // sides plus 1, first points is done twice
			float d2r = Mathf.Deg2Rad;
			shapeVerts = new Vector3[4*vertices];

			for (int i = 0; i < vertices; i++)
			{
				float outer = tHr*2;
				float inner = outer - thickness;
				float rad = d2r * (i * degrees - 30);
				float c = Mathf.Cos(rad);
				float s = Mathf.Sin(rad);

				pos0 = prevO;
				pos1 = new Vector2(outer * c, outer * s - ofs);
				pos2 = new Vector2(inner * c, inner * s - ofs);
				pos3 = prevI;
				prevO = pos1;
				prevI = pos2;

				vbo.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
			}
		}
		vbo.FillMesh(vh);
	}
}
