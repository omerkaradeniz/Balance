using System.Collections;
using UnityEngine;

public class FpsCounter : MonoBehaviour {

	public Rect startRect = new Rect(0, 0, 80, 60); // The rect the window is initially displayed at.
	public bool updateColor = true; // Do you want the color to change if the FPS gets low
	public bool allowDrag = true; // Do you want to allow the dragging of the FPS window
	public float frequency = 0.5F; // The update frequency of the fps
	public int nbDecimal = 1; // How many decimal do you want to display

	private float accum = 0f; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private Color color = Color.white; // The color of the GUI, depending of the FPS ( R < 10, Y < 30, G >= 30 )
	private string sFPS = ""; // The fps formatted into a string.
	private GUIStyle style; // The style the text will be displayed at, based en defaultSkin.label.

	void Start() {
		StartCoroutine(FPS());
	}

	void Update() {
		accum += Time.timeScale / Time.deltaTime;
		++frames;
	}

	IEnumerator FPS() {
		// Infinite loop executed every "frequency" seconds.
		while ( true ) {
			// Update the FPS
			float fps = accum / frames;
			sFPS = fps.ToString("f" + Mathf.Clamp(nbDecimal, 0, 10));

			//Update the color
			color = (fps >= 30) ? Color.green : ((fps > 10) ? Color.red : Color.yellow);

			accum = 0.0F;
			frames = 0;

			yield return new WaitForSeconds(frequency);
		}
	}

	void OnGUI() {
		// Copy the default label skin, change the color and the alignment
		if ( style == null ) {
			style = new GUIStyle(GUI.skin.label);
			style.fontSize = 24;
			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.MiddleCenter;
		}

		GUI.color = updateColor ? color : Color.white;

		startRect = GUI.Window(0, startRect, DrawWindow, "");
	}

	void DrawWindow(int windowID) {
		GUI.Label(new Rect(0, 0, startRect.width, startRect.height), sFPS, style);
		if ( allowDrag ) GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
	}
}
