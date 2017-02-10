using UnityEngine;
using System.Collections;


public class FadeScript : MonoBehaviour
{
	public static Texture2D Fade;
	public bool fadingOut = false;
	public float alphaFadeValue;
	public float fadeSpeed = 2;
	public Texture2D blackscreen;
	public bool fadein;
	
	// Use this for initialization
	void Start ()
	{
		if(fadein){
			alphaFadeValue = 1;
		}

		if(!fadein){
			alphaFadeValue = 0;
		}
	}
	void Update(){
		if (fadingOut) {
			alphaFadeValue += Mathf.Clamp01(Time.deltaTime / fadeSpeed);
				} 
		else if (fadein) {
				alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / fadeSpeed);
				}
		}
	
	// Update is called once per frame
	void OnGUI ()
	{

		
		GUI.color = new Color(0, 0, 0, alphaFadeValue);
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), blackscreen);
	}

	public void FadeOut(){
		fadingOut = true;
		}
	
}