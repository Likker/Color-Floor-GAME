using UnityEngine;
using System.Collections;

public enum EStateCube
{
    NOTHING,
    GOALMODE,
    SLOWMODE,
    DESTROY
}


public class Cube : MonoBehaviour {

    private Color GreenCustom = new Color(9 / 255.0f, 255 / 255.0f, 0 / 255.0f);
    private Color BeginningColor;

    private EStateCube cubeState;
    private float timeLeft;
    private bool resetColor = false;
    private float timeToDestroy = 1.25f;

	// Use this for initialization
	void Start () {
        cubeState = EStateCube.NOTHING;
        timeLeft = GamePlay.timeBetweenEachCubeApparition;
 
        if (!transform.parent.name.Contains("Cube"))
        {   
            GameObject child = GameObject.Instantiate(this.gameObject);
            child.transform.parent = this.transform;
            Destroy(GetComponent<Renderer>());
            Destroy(child.GetComponents<BoxCollider>()[1]);
            Destroy(child.GetComponent<Cube>());
            BeginningColor = transform.GetChild(0).GetComponent<Renderer>().material.color;
        }

	}
	
	// Update is called once per frame
    void Update () {

        if (cubeState == EStateCube.GOALMODE)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
                OnDeactivate();
        }
        SwitchColor();
	}

    public virtual void OnActivateGoal()
    {
        transform.parent.GetComponent<Score>().NewGoalCube();
        cubeState = EStateCube.GOALMODE;
    }

    public virtual void OnDeactivate()
    {
        resetColor = !resetColor;
        cubeState = EStateCube.NOTHING;
    }

    public void OnDestroyObject()
    {
        StartCoroutine("ShrinkAndDestroy");
    }

    public virtual void SwitchColor()
    {
        switch (cubeState)
        {
            case EStateCube.GOALMODE:
                transform.GetChild(0).GetComponent<Renderer>().material.color = Color.Lerp(transform.GetChild(0).GetComponent<Renderer>().material.color, GreenCustom, Time.deltaTime * 2);
                break;
            case EStateCube.NOTHING:
                if (resetColor)
                {
                    transform.GetChild(0).GetComponent<Renderer>().material.color = Color.Lerp(transform.GetChild(0).GetComponent<Renderer>().material.color, BeginningColor, Time.deltaTime * 4);
                    if (transform.GetChild(0).GetComponent<Renderer>().material.color == BeginningColor)
                        resetColor = !resetColor;
                }
                break;

        }
    }

    void OnTriggerEnter()
    {

        switch (cubeState)
        {
            case EStateCube.GOALMODE:
                // Win some points
                OnDestroyObject();
                transform.parent.GetComponent<Score>().TouchedCube();
                cubeState = EStateCube.DESTROY;
                break;
        }
    }

    IEnumerator ShrinkAndDestroy()
    {
        
        while (transform.GetChild(0).transform.localScale.x > 0.05f)
        {
            if (!PlayerController.pause)
                transform.GetChild(0).transform.localScale = Vector3.Lerp(transform.GetChild(0).transform.localScale, new Vector3(0, 1, 0), Time.deltaTime * timeToDestroy);
            yield return null;
        }

        Destroy(gameObject);

    }
    
}
