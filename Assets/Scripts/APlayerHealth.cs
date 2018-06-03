using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APlayerHealth : MonoBehaviour {
    public float health = 100f;
    public float repeatDamagePeriod = 2f;
    public AudioClip[] ouchClips;
    public float hurtForce = 10f;
    public float damageAmount = 10f;
    private SpriteRenderer healthBar;
    private float lastHitTime;
    private Vector3 healthScale;
    private PlayerControl playerControl;
    private Animator anim;	
	// Use this for initialization
	void Start() {
		playerControl = GetComponent<PlayerControl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        healthScale = healthBar.transform.localScale;
	}
	
	
    //碰撞检测
    void OnCollisionEnter2D(Collision2D col)
    {
        //碰到敌人时
        if(col.gameObject.tag == "Enemy")
        {
            //若在可攻击的时间内（受上段攻击时间后，英雄会有一个无敌时间 即 repeatDamagePeriod）
            if(Time.time > lastHitTime + repeatDamagePeriod)
            {
                //若英雄还活着
                if(health > 0f)
                {
                    //对他扣血，并且将敌人推远
                    TakeDamage(col.transform);
                    //重置上次打击时间
                    lastHitTime = Time.time ;
                }
                    // 英雄没血条
                else
                {
                   // 获得英雄身上所有的碰撞器，并且将它们的istrigger属性改为true，使他们掉落；
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach(Collider2D c in cols)
                    {
                        c.isTrigger = true;
                    }
                    SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
                    foreach(SpriteRenderer s in spr)
                    {
                        s.sortingLayerName = "UI";
                    }
                    //兄弟，死人可不会动
                    GetComponent<PlayerControl>().enabled = false;
                    //兄弟，死人可不能开炮
                    GetComponentInChildren<Gun>().enabled = false;
                    anim.SetTrigger("Die");
                }
            }
        }
    }
	void TakeDamage(Transform enemy) {
        //受伤时，角色不能进行跳跃
        playerControl.jump = false;
        //创建一个从敌人到玩家的向量
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
        //在向量的方向对玩家加力
        GetComponent<Rigidbody2D>().AddForce(hurtForce * hurtVector);
        //减血
        health -= damageAmount;
        //缩短血条
        UpdateHealthBar();
        //随机发出惨叫 “啊” “哦” “痛”~~~~~~~—.—#~~~~~~~
        int i = Random.Range(0, ouchClips.Length);
        AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);

	}
    public void UpdateHealthBar()
    {
        //血条越少颜色越红
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f ,1 , 1);
    }
}
