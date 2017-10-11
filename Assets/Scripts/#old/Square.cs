using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Square : Graphic {

	private int id;
	private int xPos;
	private int yPos;
	private int inSet;
	private Color color;
	private Sprite sprite;
	private GameObject transform;
	public Vector2[] vertices;


	public Square(int nrOfSquare, int xPosition, int yPosition, int squareSet, Color squareColor, Sprite squareSprite, GameObject squareTransform)
	{
		id = nrOfSquare;
		xPos = xPosition;
		yPos = yPosition;
		inSet = squareSet;
		color = squareColor;
		sprite = squareSprite;
		transform = squareTransform;
	}

	public void Create()
	{
		GameObject square = Instantiate(transform, new Vector3(xPos, yPos, 1), Quaternion.identity) as GameObject;
		transform.GetComponent<SpriteRenderer>().sprite = sprite;
		transform.GetComponent<SpriteRenderer>().color = color;
		square.transform.parent = GameObject.Find ("Canvas").transform;
	}
}
