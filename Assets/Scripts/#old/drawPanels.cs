using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class drawPanels : MonoBehaviour {

    private GameObject canvas;

    void Awake ()
    {
        canvas = gameObject;
    }

	// Use this for initialization
	void Start () {

        GameObject panel = new GameObject("Panel");
        panel.AddComponent<CanvasRenderer>();
        Image i = panel.AddComponent<Image>();
        i.color = Color.red;
        panel.AddComponent<BoxCollider2D>();
        panel.transform.SetParent(canvas.transform, false);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

