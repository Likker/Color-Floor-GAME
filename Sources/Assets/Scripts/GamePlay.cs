using UnityEngine;
using System.Collections;

public enum EStateGame
{
    MENU,
    PAUSE,
    PLAY
}
    
public class GamePlay : MonoBehaviour {

    static public float timeBetweenEachCubeApparition = 7;
    public GameObject sphere;
    public GameObject canvas;

    private EStateGame stateOfTheGame;

    public static bool gameLaunched = false;
    private float timeLeft;

    private GameObject cube;

	// Use this for initialization
	void Start () {
        timeLeft = 3;
        stateOfTheGame = EStateGame.MENU;
        sphere.GetComponent<Rigidbody>().isKinematic = true;
   	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && (stateOfTheGame == EStateGame.PLAY || stateOfTheGame == EStateGame.PAUSE))
        {
            PauseGame();              
        }
        if (Input.GetKeyDown(KeyCode.Space) && stateOfTheGame != EStateGame.PLAY)
            LaunchGame();
            

        if (stateOfTheGame == EStateGame.PLAY)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = timeBetweenEachCubeApparition;
                GetRandomChild().GetComponent<Cube>().OnActivateGoal();
            }
        }

        // select a randomchild and Call OnActivate
	}

    public void LaunchGame()
    {
        stateOfTheGame = EStateGame.PLAY;
        sphere.GetComponent<Rigidbody>().isKinematic = false;
        canvas.SetActive(false);
        GetComponent<CountTimer>().SwapTimerState();
        GetComponent<Score>().ChangeGameState();
    }

    private void PauseGame()
    {
        if (stateOfTheGame == EStateGame.PLAY)
        {
            sphere.GetComponent<BallController>().OnPauseGame();
            GetComponent<PlayerController>().OnPauseGame();
            stateOfTheGame = EStateGame.PAUSE;
            GetComponent<CountTimer>().SwapTimerState();
        }
        else
        {
            GetComponent<PlayerController>().OnResumeGame();
            sphere.GetComponent<BallController>().OnResumeGame();
            stateOfTheGame = EStateGame.PLAY;
            GetComponent<CountTimer>().SwapTimerState();
        }
    }

    private Transform GetRandomChild()
    {
        return transform.GetChild(Random.Range(0, transform.childCount));
    }
       
}
