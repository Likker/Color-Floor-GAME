using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountTimer : MonoBehaviour {

    public float timeOfGameInSeconds = 0.1f;
    public Text timerLabel;
    public GameObject endgame;

    private float timeLeft;
    private bool timerActivate = false;
	// Use this for initialization
	void Start () {
        timeLeft = timeOfGameInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
	
        if (timerActivate)
        {
            timeLeft -= Time.deltaTime;
            DisplayLabel();
            if (timeLeft <= 0)
                EndTimer();
        }
       
	}

    public void SwapTimerState()
    {
        timerActivate = !timerActivate;

    }
       

    private void EndTimer()
    {
        endgame.GetComponent<EndGame>().EndTheGame();
        Debug.Log("end");
    }

    private void DisplayLabel()
    {
        int secondes = (int)timeLeft;
        timerLabel.text = secondes.ToString();
    }

}
