using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text scoreLabel;
    public int score = 0;

    private float time = 0;
    private bool IsThereAGoal = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (IsThereAGoal)
        {
            time += Time.deltaTime;
        }
	}
       
    public void ChangeGameState()
    {
        DisplayGameScore();
    }
        
    public void NewGoalCube()
    {
        time = 0;
        IsThereAGoal = true;
    }

    public void TouchedCube()
    {
        score += 100;
        score += (int)((1/ time) * 100);
        Debug.Log("Time To go to cube : " + time);
        Debug.Log("Points earn : " + (1/ time) * 100);
        DisplayGameScore();
    }

    void DisplayGameScore()
    {
        scoreLabel.text = score.ToString();
    }
}
