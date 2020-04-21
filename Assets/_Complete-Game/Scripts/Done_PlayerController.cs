using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
    public float rollSpeed;
	public float tilt;
    public float rolltilt;
	public Done_Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawnL;
	public Transform shotSpawnR;
	public float fireRate;
    public float rollRate;
	public SimpleTouchAreaButton areaButton;
    public Joystick joystick;
    public SimpleTouchAreaButton shipRoll;
    public Quaternion shotL;
    public Quaternion shotR;
    public bool extraShot;

    private float regularSpeed;
    private float regulartilt;
	private float nextFire;
    private float nextRoll;
    private Quaternion calibrationQuaternion;
    Animator animator;
    Rigidbody rb;

    void Start () {
        CalibrateAccelerometer ();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        regularSpeed = speed;
        regulartilt = tilt;
        //shipTrail.emitting = false;
    }	
	void Update ()
	{
		if (areaButton.CanFire () && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
            Shots(shot, shotSpawn);
            if (extraShot)
            {
                Shots(shot, shotSpawnL);
                Shots(shot, shotSpawnR);
            }

            //audio.Play ();            
            GetComponent<AudioSource>().Play ();
		}
	}
    void Shots(GameObject inst, Transform objeto)
    {
        GameObject shots = (GameObject)Instantiate(inst, objeto.position, objeto.rotation);
        Destroy(shots, 3f);
    }	
	//Used to calibrate the Input.acceleration input
    void CalibrateAccelerometer () {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
	    Vector3 FixAcceleration (Vector3 acceleration) {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
    }

	void FixedUpdate ()
    {        
        Vector3 moveVector = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
        animator.SetFloat("Move", joystick.Direction.x);
        Roll();
        rb.velocity = moveVector * speed;
        
        rb.position = new Vector3
        (
           Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 0.0f, 
           Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );        
        rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }
    public void Roll()
    {
        if (shipRoll.touched)// && Time.time > nextRoll)
        {
            //shipTrail.emitting = true;
            speed = rollSpeed;
            tilt = rolltilt;
            nextRoll = Time.time + rollRate;
           // float moving = animator.GetFloat("Move");
           /*
            if (moving < 0)
            {
                speed = rollSpeed;
                //animator.SetTrigger("RollL");
                shipTrail.emitting = !shipTrail.emitting;
                //StartCoroutine(TimeToRoll());
            }
            else if (moving > 0)
            {
                speed = rollSpeed;
                //animator.SetTrigger("RollR");
                shipTrail.emitting = !shipTrail.emitting;
                //StartCoroutine(TimeToRoll());
            }*/
        }
        else
        {
            speed = regularSpeed;
            tilt = regulartilt;
            //shipTrail.emitting = false;
        }

    }
    IEnumerator TimeToRoll()
    {
        yield return new WaitForSeconds(1f);
        speed = regularSpeed;
        //shipTrail.emitting = !shipTrail.emitting;
    }
}
