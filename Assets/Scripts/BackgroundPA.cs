using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPA : MonoBehaviour {

    public Transform[] backgrounds;
    public float parallaxScale;
    public float parallaxReductionFactor;
    public float smoothing;

    public Transform cam;
    public Vector3 previousCamPos;
	// Use this for initialization
    void Awake()
    {
        cam = Camera.main.transform;
    }
    void Start () {
        previousCamPos = cam.position;
	}
	
	// Update is called once per frame
	void Update () {
        //float parallax = ();

    }
}
