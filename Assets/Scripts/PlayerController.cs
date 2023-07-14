using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float drag;
	public float xSensitivity;
	public float ySensitivity;
	public GameObject flashlight;
	public GameObject holdingTarget;
	public float lerpSpeed;
	public GameObject crosshair;
	public Texture crosshairUnselected;
	public Texture crosshairSelected;
	public LayerMask itemsMask;
	public float reach;
	public float forceAmplifier;
	public TMP_Text healthText;
	public int health;


	private GameObject holdingObj;
	private Rigidbody rb;
	private GameObject cam;
	private float yValue;
	private Vector3 lastWorldPos;
	private Vector3 lastWorldRot;
	private Animator anim;




	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		LockCursor(true);
		cam = Camera.main.gameObject;
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		flashlight.transform.position = holdingTarget.transform.position;
		flashlight.transform.rotation = Quaternion.Lerp(cam.transform.rotation, holdingTarget.transform.rotation, lerpSpeed * Time.deltaTime);


		healthText.text = "Health: " + health.ToString();
		if (health < 0) {
			SceneManager.LoadScene("");
		}

		rb.AddRelativeForce(new Vector3(Input.GetAxisRaw("Horizontal")* speed, 0, Input.GetAxisRaw("Vertical") * speed), ForceMode.Impulse);
		rb.AddForce(new Vector3(rb.velocity.x  * -drag, 0, rb.velocity.z  * -drag), ForceMode.Impulse);
		transform.Rotate(new Vector3(0,xSensitivity * Time.timeScale * Input.GetAxis("Mouse X"),0));
        cam.transform.localRotation = Quaternion.Euler(yValue, 0, 0);
        yValue -= Input.GetAxis("Mouse Y") * ySensitivity * Time.timeScale;        
        if (yValue > 90)
        {
            yValue = 90;
        }
        else if (yValue < -90)
        {
            yValue = -90;
        }
		if (Input.GetAxisRaw("Horizontal") * speed != 0|| Input.GetAxisRaw("Vertical") * speed != 0) {
			anim.SetBool("Moving",true);
		}
		else {anim.SetBool("Moving",false);}

		if (Input.GetMouseButtonUp(1)) {
			if (holdingObj != null)
			{


				holdingObj.transform.SetParent(null);
				holdingObj.GetComponent<Collider>().enabled = true;
				holdingObj.GetComponent<Rigidbody>().isKinematic = false;
				holdingObj.GetComponent<Rigidbody>().AddTorque((holdingObj.transform.rotation.eulerAngles - lastWorldRot) * forceAmplifier, ForceMode.Impulse);
				holdingObj.GetComponent<Rigidbody>().AddForce((holdingObj.transform.position - lastWorldPos) * forceAmplifier, ForceMode.Impulse);
				holdingObj = null;
			}
		}


		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
		if (Physics.Raycast(ray, out hit, reach, itemsMask))
		{
			if (Input.GetMouseButtonDown(1)) {
				holdingObj = hit.collider.gameObject;
				holdingObj.GetComponent<Collider>().enabled = false;
				holdingObj.GetComponent<Rigidbody>().isKinematic = true;
				hit.collider.transform.SetParent(holdingTarget.transform);
			}
			
			Debug.DrawRay(transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
			crosshair.GetComponent<RawImage>().texture = crosshairSelected;
		}
		else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
			crosshair.GetComponent<RawImage>().texture = crosshairUnselected;
        }
		if (holdingObj != null)
		{
			lastWorldPos = holdingObj.transform.position;
			lastWorldRot = holdingObj.transform.rotation.eulerAngles;
		}
	}



	void LockCursor(bool doLock) {
		if (doLock == true) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
