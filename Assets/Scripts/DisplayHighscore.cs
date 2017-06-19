using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscore : MonoBehaviour {

	[Header("ScoreText")]
	public Text scoreText;
	public Text highscoreText;

	private void Start(){
		scoreText.text = "Your score was " + PlayerPrefs.GetInt ("currentScore", 0);
		highscoreText.text = "Current Highscore: " + PlayerPrefs.GetInt ("highscore", 0);
	}
}
