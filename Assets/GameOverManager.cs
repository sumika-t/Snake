using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadSceneを使うために必要

public class GameOverManager : MonoBehaviour {

	public GUIText scoreGUIText;
	public GUIText highScoreGUIText;

	int highScore;

	private string highScoreKey = "highScore";

	// Use this for initialization
	void Start () {
		highScore = PlayerPrefs.GetInt (highScoreKey, 0);
		scoreGUIText.text = "Score : " + Score.score.ToString ();
		highScoreGUIText.text = "HighScore : " + highScore.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X))
		{
			SceneManager.LoadScene("TitleScene");
		}
	}
}
