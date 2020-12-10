using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandlerAntsGrasshoppers : MonoBehaviour
{

	public GameObject TimeText;
	public int Timeframe = 300;
	private int TimeTics;
	private int TimeSeconds;

	public static int antScore;
	public static int grasshopperScore;

	public GameObject antScoreText;
	public GameObject grasshopperScoreText;

	//public GameObject GameOverText;
	private string winnerBug;

	public GameObject BG_ANTwins;
	public GameObject BG_GHwins;
	public GameObject BG_tie;


    // Start is called before the first frame update
    void Start()
    {
		TimeSeconds = 0; 
		TimeTics = 0;
		updateScore("ant", 0);
		updateScore("grasshopper", 0);
		updateGameOver();
    }

       void Update () {     // always include a way to quit the game :)
              if (Input.GetKey("escape")) {
                    Application.Quit ();
                    }
             }

	void FixedUpdate () {
		TimeTics += 1;
		if (TimeTics >= 60) {
			if (TimeSeconds <= (Timeframe -1)){ 
				TimeSeconds += 1;
				updateTime();
			}
			TimeTics = 0;
		}

             }

	public void updateScore(string playerName, int newScore){
		if (playerName == "ant"){
			antScore += newScore;
			Text scoreTextAnt = antScoreText.GetComponent<Text>();
			scoreTextAnt.text = "Ant Score: " + antScore;
		}
		if (playerName == "grasshopper"){
Debug.Log("I am trying to update the grasshopper score by this much: " + newScore);
			grasshopperScore += newScore;
			Text scoreTextgrasshopper = grasshopperScoreText.GetComponent<Text>();
			scoreTextgrasshopper.text = "Grasshopper Score: " + grasshopperScore;
		}

	}

	public void updateTime(){
		Text TimeTextTemp = TimeText.GetComponent<Text>();
		TimeTextTemp.text = "TIME LEFT: " + (Timeframe - TimeSeconds);
		if (TimeSeconds >= Timeframe){
			SceneManager.LoadScene("AvG_GameOver"); 
			}
		}

	public void StartGame(){
		antScore = 0;
		grasshopperScore = 0;
		SceneManager.LoadScene("AvG_Level1"); 
	}
	public void QuitGame(){
		Application.Quit(); 
	}
	public void RestartGame(){
		antScore = 0;
		grasshopperScore = 0;
		SceneManager.LoadScene("AvG_MainMenu"); 
	}

	public void Credits(){
		SceneManager.LoadScene("AvG_Credits"); 
	}

	public void updateGameOver(){
		if (antScore == grasshopperScore){
			BG_ANTwins.SetActive(false);
			BG_GHwins.SetActive(false);
			BG_tie.SetActive(true);
			//Text GameOverTextTemp = GameOverText.GetComponent<Text>();
			//GameOverTextTemp.text = "GAME OVER! \n It's a tie!";
		}
		else if (antScore >= (grasshopperScore +1)){
			winnerBug = "Ant";
			BG_ANTwins.SetActive(true);
			BG_GHwins.SetActive(false);
			BG_tie.SetActive(false);
			//Text GameOverTextTemp = GameOverText.GetComponent<Text>();
			//GameOverTextTemp.text = "GAME OVER! \n The " + winnerBug + " wins!";
		}
		else if (grasshopperScore >= (antScore +1)){
			winnerBug = "Grasshopper";
			BG_ANTwins.SetActive(false);
			BG_GHwins.SetActive(true);
			BG_tie.SetActive(false);
			//Text GameOverTextTemp = GameOverText.GetComponent<Text>();
			//GameOverTextTemp.text = "GAME OVER! \n The " + winnerBug + " wins!";
		}

	}

}