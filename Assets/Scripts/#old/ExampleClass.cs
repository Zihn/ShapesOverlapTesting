using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
	public Material mat;
	void OnPostRender() {
		if (!mat) {
			Debug.LogError("Please Assign a material on the inspector");
			return;
		}
		GL.PushMatrix();
		mat.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(GL.QUADS);
		GL.Color(Color.red);
		GL.Vertex3(0, 0.5F, 0);
		GL.Vertex3(0.5F, 1, 0);
		GL.Vertex3(1, 0.5F, 0);
		GL.Vertex3(0.5F, 0, 0);
		GL.Color(Color.cyan);
		GL.Vertex3(0, 0, 0);
		GL.Vertex3(0, 0.25F, 0);
		GL.Vertex3(0.25F, 0.25F, 0);
		GL.Vertex3(0.25F, 0, 0);
		GL.End();
		GL.PopMatrix();
	}
}