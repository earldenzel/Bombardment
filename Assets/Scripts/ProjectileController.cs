using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProjectileController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private GameObject wind;
    private RaycastHit2D[] hits;
    private Tilemap currentMap;
    public CannonController cannon;

    public float windEffect;
    public int collateralDamageSize;
    public int baseDamage;
    
    //public GameObject projectileExplosion;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        wind = GameObject.FindGameObjectWithTag("Environment");
        currentMap = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Tilemap>();
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
        if (!cannon.OnShot)
        {
            Vector2 finalDirection = rb2d.velocity.normalized;
            rb2d.velocity = Vector2.zero;
            rb2d.drag = 100000;
            rb2d.angularDrag = 100000;

            Vector3Int gridCenterOfImpact = currentMap.WorldToCell(transform.position);
            for (int x = -collateralDamageSize; x <= collateralDamageSize; x++)
            {
                for (int y = -collateralDamageSize; y <= collateralDamageSize; y++)
                {
                    //set circle. math ftw.
                    if ((collateralDamageSize * collateralDamageSize) >= (x * x + y * y))
                    {
                        currentMap.SetTile(new Vector3Int(x + gridCenterOfImpact.x, y + gridCenterOfImpact.y, gridCenterOfImpact.z), null);
                    }
                }
            }

            if(collision.gameObject.tag == "Terrain")
            {
                hits = Physics2D.CircleCastAll(this.transform.position, collateralDamageSize * 0.08f, Vector2.zero);

                foreach (RaycastHit2D h in hits)
                {
                    if (h.transform.gameObject.tag == "Enemy")
                    {
                        float damage = (h.transform.position - this.transform.position).magnitude;

                        //provides some weak damage if hit was not exact
                        if (damage < collateralDamageSize*0.08f)
                        {
                            damage = 1 - (damage / (collateralDamageSize*0.08f));
                            damage *= baseDamage;
                            h.transform.gameObject.GetComponent<EnemyController>().Damage((int)Mathf.Round(damage/2));
                        }
                    }

                }
            }


  
        }        
    }
}
