using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandlerAntsGrasshoppers : MonoBehaviour
{

	public static int antScore;
	public static int grasshopperScore;

	public GameObject antScoreText;
	public GameObject grasshopperScoreText;

    // Start is called before the first frame update
    void Start()
    {
		updateScore("ant", antScore);
		updateScore("grasshopper", antScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void updateScore(string playerName, int newScore){
		if (playerName == "ant"){
			antScore += newScore;
			Text scoreTextAnt = antScoreText.GetComponent<Text>();
			scoreTextAnt.text = "Ant Score: " + antScore;
		}
		if (playerName == "grasshopper"){
			grasshopperScore += newScore;
			Text scoreTextgrasshopper = grasshopperScoreText.GetComponent<Text>();
			scoreTextgrasshopper.text = "Grasshopper Score: " + grasshopperScore;
		}

	}


}