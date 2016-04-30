using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float timeToRotate = 1;
    public float maxInclinaison = 20;

    public static bool pause = false;
	// Use this for initialization
	void Start () {
	
	}
	
    /*
     * Need to optimize Rotation for a specic angle ( mobile phone version )
     */

	// Update is called once per frame
	void Update () {
        if (!pause)
        {
            if (checkUp())
                return;
            else if (checkDown())
                return;
            else if (checkLeft())
                return;
            else if (checkRight())
                return;
            ResetRotation();
        }

	}

    private bool checkUp()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {

            Quaternion rot = transform.localRotation;
            rot.eulerAngles = new Vector3(Mathf.Lerp(rot.eulerAngles.x > 180 ? rot.eulerAngles.x - 360 : rot.eulerAngles.x, maxInclinaison, Time.deltaTime * timeToRotate), rot.eulerAngles.y, Mathf.Lerp(rot.eulerAngles.z > 180 ? rot.eulerAngles.z - 360 : rot.eulerAngles.z, maxInclinaison, Time.deltaTime * timeToRotate));
            transform.localRotation = rot;

            return true;
        }
        return false;
    }

    private bool checkDown()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Quaternion rot = transform.localRotation;
            rot.eulerAngles = new Vector3(Mathf.Lerp(rot.eulerAngles.x > 180 ? rot.eulerAngles.x - 360 : rot.eulerAngles.x , - maxInclinaison / 2, Time.deltaTime * timeToRotate), 0, Mathf.Lerp(rot.eulerAngles.z > 180 ? rot.eulerAngles.z - 360 : rot.eulerAngles.z, - maxInclinaison / 2, Time.deltaTime * timeToRotate));


            transform.localRotation = rot;
            return true;
        }
        return false;
    }

    private bool checkLeft()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Quaternion rot = transform.localRotation;
            rot.eulerAngles = new Vector3(Mathf.Lerp(rot.eulerAngles.x > 180 ? rot.eulerAngles.x - 360 : rot.eulerAngles.x, -maxInclinaison / 2, Time.deltaTime * timeToRotate), rot.eulerAngles.y, Mathf.Lerp(rot.eulerAngles.z > 180 ? rot.eulerAngles.z - 360 : rot.eulerAngles.z, maxInclinaison / 2, Time.deltaTime * timeToRotate));
            transform.localRotation = rot;
            return true;
        }
        return false;
    }

    private bool checkRight()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Quaternion rot = transform.localRotation;
            rot.eulerAngles = new Vector3(Mathf.Lerp(rot.eulerAngles.x > 180 ? rot.eulerAngles.x - 360 : rot.eulerAngles.x, maxInclinaison / 2, Time.deltaTime * timeToRotate), rot.eulerAngles.y, Mathf.Lerp(rot.eulerAngles.z > 180 ? rot.eulerAngles.z - 360 : rot.eulerAngles.z, -maxInclinaison / 2, Time.deltaTime * timeToRotate));
            transform.localRotation = rot;
            return true;
        }
        return false;
    }

    private void ResetRotation()
    {
        Quaternion rot = transform.localRotation;
        rot.eulerAngles = new Vector3(Mathf.Lerp(rot.eulerAngles.x > 180 ? rot.eulerAngles.x - 360 : rot.eulerAngles.x, 0, Time.deltaTime * timeToRotate), rot.eulerAngles.y, Mathf.Lerp(rot.eulerAngles.z > 180 ? rot.eulerAngles.z - 360 : rot.eulerAngles.z, 0, Time.deltaTime * timeToRotate));
        transform.localRotation = rot;
    }

    public void OnPauseGame()
    {
        pause = true;
    }

    public void OnResumeGame()
    {
        pause = false;
    }
}
