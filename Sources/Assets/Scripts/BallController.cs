using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void OnPauseGame()
    {
        savedVelocity = rb.velocity;
        savedAngularVelocity = rb.angularVelocity;
        rb.isKinematic = true;
    }

    public void OnResumeGame()
    {
        rb.isKinematic = false;
        rb.AddForce(savedVelocity, ForceMode.VelocityChange);
        rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
    }
}
