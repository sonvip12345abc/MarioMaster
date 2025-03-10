using UnityEngine;
using System.Collections;

public class BanDan : MonoBehaviour
{

	public GameObject projectile;
	public Vector2 velocity;
	bool canShoot = true;
	public Vector2 offset = new Vector2(0.4f, 0.1f);
	public float cooldown = 1f;


	// Use this for initialization
	void Start()
	{

	}
    private void Awake()
    {
	
	}
    // Update is called once per frame
    void Update()
	{
		if (FindObjectOfType<MarioScript>().CapDo == 4)
		{

			if (Input.GetKeyDown(KeyCode.Z) && canShoot)
			{

				GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

				go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);


				StartCoroutine(CanShoot());



			}
		}


	}


	IEnumerator CanShoot()
	{
		canShoot = false;
		yield return new WaitForSeconds(cooldown);
		canShoot = true;


	}
}