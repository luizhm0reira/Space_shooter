using UnityEngine;
using System.Collections;

public class Done_WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawnL;
	//public Transform shotSpawnR;
	public float fireRate;
	public float delay;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		//Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation);
		//Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation);
		GetComponent<AudioSource>().Play();
	}
}
