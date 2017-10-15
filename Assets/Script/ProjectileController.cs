using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private GameObject wind;
    public float windEffect;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        wind = GameObject.FindGameObjectWithTag("Environment");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb2d.velocity != Vector2.zero)
        {
            //constant force object applies only when projectile is moving
            rb2d.GetComponent<ConstantForce2D>().force = windEffect * (Vector2) wind.GetComponent<WindSpawner>().Wind;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        rb2d.velocity = Vector2.zero;
        rb2d.drag = 100000;
        rb2d.angularDrag = 100000;
    }
}
