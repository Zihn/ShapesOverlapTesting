using UnityEngine;
using System.Collections;

public class generateTexture : MonoBehaviour {

	public int mapHeight = 4;
	public int mapWidth = 4;

	public int posX = 0;
	public int posY = 0;
	public int width = 1;
	public int height = 1;

	private Renderer textureRenderer;
	private Color[] colourMap;

	void Awake()
	{
		colourMap = new Color[mapWidth * mapHeight];
		textureRenderer = gameObject.GetComponent<MeshRenderer> ();
		GenerateBase ();
		GenerateShape ();
	}

	void GenerateBase() 
	{
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				colourMap [y * mapWidth + x] = Color.white;
			}
		}
		DrawTexture (TextureFromColourMap (colourMap));
	}

	public void GenerateShape()
	{
		// Make this OOP approach -> shape.draw()
		for (int y = posX; y < height; y++) {
			for (int x = posY; x < width; x++) {
				colourMap [y * mapWidth + x] = Color.red;
			}
		}
		DrawTexture (TextureFromColourMap (colourMap));
	}

	Texture2D TextureFromColourMap(Color[] colourMap) {
		Texture2D texture = new Texture2D (mapWidth, mapHeight);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels (colourMap);
		texture.Apply ();
		return texture;
	}

	void DrawTexture(Texture2D texture) {
		
		textureRenderer.sharedMaterial.mainTexture = texture;
		textureRenderer.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}

}