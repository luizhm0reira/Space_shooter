using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawnL;
	public Transform shotSpawnR;
	public float fireRate;
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;
	 
	private float nextFire;
    private Quaternion calibrationQuaternion;
	public Quaternion shotL;
	public Quaternion shotR;
	public Quaternion shotO;


    void Start () {
        CalibrateAccelerometer (); 
    }
	
	void Update ()
	{
		if (areaButton.CanFire () && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			shotO = shotSpawn.rotation;


            Shots(shot, shotSpawn);
            //GameObject shot1 = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //Destroy(shot1);
            Shots(shot, shotSpawnL);
            Shots(shot, shotSpawnR);            
            
            //audio.Play ();            
            GetComponent<AudioSource>().Play ();
		}
	}

    void Shots(GameObject inst, Transform objeto)
    {
        GameObject shots = (GameObject)Instantiate(inst, objeto.position, objeto.rotation);
        Destroy(shots, 5f);
    }
	
	//Used to calibrate the Iput.acceleration input
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
//      float moveHorizontal = Input.GetAxis ("Horizontal");
//      float moveVertical = Input.GetAxis ("Vertical");
        
//      Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

//      Vector3 accelerationRaw = Input.acceleration;
        //      Vector3 acceleration = FixAcceleration (accelerationRaw);
//      Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);


        Vector2 direction = touchPad.GetDirection ();
        Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
		Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = movement * speed;
        
        rb.position = new Vector3
        (
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );
        
        rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
