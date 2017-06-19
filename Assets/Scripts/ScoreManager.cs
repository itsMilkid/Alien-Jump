using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int score;

	[Header("Display")]
	public Text scoreText;
	public Text highscoreText;

	private void Start(){
		highscoreText.text = " Highscore: " + PlayerPrefs.GetInt ("highscore", 0);
	}

	private void Update(){
		scoreText.text = score.ToString ();

		StoreCurrentScore (score);
		StoreHighscore (score);
	}

	private void StoreCurrentScore(int _score){
		PlayerPrefs.SetInt ("currentScore", _score);
		PlayerPrefs.Save ();
	}

	private void StoreHighscore(int _score){
		int currentHighscore = PlayerPrefs.GetInt ("highscore", 0);

		if (_score > currentHighscore) {
			PlayerPrefs.SetInt ("highscore", _score);
			highscoreText.text = " New Highscore: " + score.ToString ();
		}

		PlayerPrefs.Save ();
	}
}
