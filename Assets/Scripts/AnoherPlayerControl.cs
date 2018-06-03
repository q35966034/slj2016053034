using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnoherPlayerControl : MonoBehaviour {
    //水平移动
    public float maxSpeed = 5f;
    public float moveForce = 365f;
    [HideInInspector]public bool bFaceright = true;
    //跳跃
    public float jumpForce = 1000f;
    public Transform mGroundCheck;
    private bool grounded = false;
    private bool bJump = false;
    private Animator anim;
    // 动画
    /*public AudioClip[] jumpClips;
    private Animator anim;*/

    private Rigidbody2D herobody;
	// Use this for initialization
	void Start () {
        herobody = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("groundObject");
        anim = GetComponent<Animator>();

    }
	void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(h));
        if ( h * herobody.velocity.x < maxSpeed )
        {
            herobody.AddForce(Vector2.right * h * moveForce);

        }
        if(Mathf.Abs(herobody.velocity.x) > maxSpeed)
        {
            herobody.velocity = new Vector2(Mathf.Sign(herobody.velocity.x) * maxSpeed, herobody.velocity.y);
        }
        if(h>0 && !bFaceright)
        {
            flip();
        }
        if(h<0 && bFaceright)
        {
            flip();
        }
        if(bJump)
        {
            /*anim.SetTrigger("Jump");

            int i = Random.Range(0, jumpClips.Length);

            AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);*/
            anim.SetTrigger("jump");
            herobody.AddForce(new Vector2(0f, jumpForce));
            bJump = false;
        }
    }
    // Update is called once per frame
    void Update () {
        grounded = Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("ground"));
        if(grounded && Input.GetButtonDown("Jump"))
        {
            bJump = true;
        }
	}
    void flip()
    {
        bFaceright = !bFaceright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
