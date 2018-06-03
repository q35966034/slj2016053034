using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arocket : MonoBehaviour {
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2);

    }
	void onExplosion()
    {
        Quaternion randomRotation = Quaternion.Euler(0,0,Random.Range(0,360));
        Instantiate(explosion,transform.position,randomRotation);
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != "Player")
        {
            // Instantiate the explosion and destroy the rocket.
            onExplosion();
            Destroy(gameObject);
        }
    }

}
