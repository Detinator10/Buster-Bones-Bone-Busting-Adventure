using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameWideScript : MonoBehaviour {

	public int ReachedLevel;
	public int thislevel;
	public float thisleveltime;
	public float[] levelTimes;
	public bool paused;
	public int maxlevel;

	public static bool needtoload;
	public string leveltoload;



	public void Start(){
				if (!Application.genuine) {
			Application.LoadLevel("HackedGame");
				}

				if (!needtoload) {
						levelTimes = new float[maxlevel + 1];
						DontDestroyOnLoad (this.gameObject);

			if (PlayerPrefs.HasKey ("Level " + maxlevel)) {
								Debug.Log ("restoring data");
								ReachedLevel = PlayerPrefs.GetInt ("reachedlevel");
				for(int i = 1; i <= maxlevel; i++) {
						
										leveltoload = "Level " + i;
										levelTimes[i] = (PlayerPrefs.GetFloat (leveltoload));
										
				}
				
								if (levelTimes [maxlevel] == PlayerPrefs.GetFloat ("Level " + maxlevel)) {
										print ("data restoration successful");
										Debug.Log ("data restored");

								}
								needtoload = true;
						} else if (!needtoload) {
								print ("inititating data");
								PlayerPrefs.SetInt ("reachedlevel", ReachedLevel);
								for (int i = 1; i <=  maxlevel; i++) {
										if (!PlayerPrefs.HasKey ("Level " + i)) {
												PlayerPrefs.SetFloat ("Level " + i, levelTimes [i]);
										}
								}
								needtoload = true;
						}
						print ("reachedlev" + ReachedLevel);

				}

		}

	//public int SetVariable
	IEnumerator Wait(float wait) {
		yield return new WaitForSeconds(wait);
		Application.LoadLevel ("GameOverScreen");
	}
			

	public void GameOver(){
		StartCoroutine (Wait(1f));
		}


	public void Save(){
		Debug.Log ("saving");
				PlayerPrefs.SetInt ("reachedlevel", ReachedLevel);
				for (int i = 1; i <=  maxlevel; i++) {
						if (PlayerPrefs.HasKey ("Level " + i)) {
								PlayerPrefs.SetFloat ("Level " + i, levelTimes[i]);
			}
			
				}
		PlayerPrefs.Save();
		}
		
		public void Delete(){
		PlayerPrefs.DeleteAll ();
		}

	public void Pause(){
		paused = true;
		Time.timeScale = 0;
	}

	public void Resume(){
		paused = false;
		Time.timeScale = 1;
	}
	public void Restart(){
		Application.LoadLevel ("Level " + thislevel);
	}

}
