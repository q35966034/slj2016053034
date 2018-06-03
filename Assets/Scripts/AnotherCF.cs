using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherCF : MonoBehaviour {
    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;

    public Vector2 maxXandY;
    public Vector2 minXandY;

    public Transform player;
	// Use this for initialization
	void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }
    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }
	// Update is called once per frame
	void LateUpdate() {
        TrackPlayer();
	}
    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if(CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }
        if(CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
        }
        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
