using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    private int score = 0;
    private Score scoreObj;
    private CountTimer timer;
    public Text highscoreUI;
    public InputField input;

	// Use this for initialization
	void Start () {

        scoreObj = GameObject.FindWithTag("Player").GetComponent<Score>();
        timer = GameObject.FindWithTag("Player").GetComponent<CountTimer>();
        for (int i = 0; i < 5; i++)
        {
            //Get the highScore from 1 - 5
            string highScoreKey = "HighScore" + (i + 1).ToString();


            int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
           // string name = PlayerPrefs.GetString(highScoreKey+"name", "Unknown");
            if (highScore != 0)
                highscoreUI.text += highScore.ToString() + "\n";
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            SaveHighScore();
        }
	}

    void OnTriggerEnter()
    {

        EndTheGame();
    }

    public void EndTheGame()
    {
        score = scoreObj.score;
        //input.gameObject.SetActive(true);
        timer.SwapTimerState();
        SaveHighScore();
    }

    private void SaveHighScore()
    {
        bool addedhighscore = false;
        int[] highScores = new int[5];

        for (int i = 0; i < highScores.Length; i++)
        {

            //Get the highScore from 1 - 5
            string highScoreKey = "HighScore"+(i+1).ToString();
            int highScore = PlayerPrefs.GetInt(highScoreKey,0);
            string tmpName = ""; 

            if(score>highScore)
            {
                int temp = highScore;
                PlayerPrefs.SetInt(highScoreKey, score);
                addedhighscore = true;       
                score = temp;
            }
             
        }
        Application.LoadLevel(0);
    }

    public void AddNameHighScore()
    {
        SaveHighScore();
    }
}
