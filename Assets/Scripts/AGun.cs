 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGun : MonoBehaviour {
    private AnoherPlayerControl playercontrol;
    public float speed = 20f;
    public Rigidbody2D rocket;
	// Use this for initialization
	void Start () {
        playercontrol = transform.root.GetComponent<AnoherPlayerControl>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if (playercontrol.bFaceright)
            {
               Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(speed, 0);
            }
            else
            {
                Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, -180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-speed, 0);
            }
        }

    }
}
