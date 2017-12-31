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
    public GameObject attacker;

    public float windEffect;
    public int collateralDamageSize;
    public int baseDamage;
    public AudioClip spawnAudio;
    public AudioClip hitEnemyAudio;
    public AudioClip hitGroundAudio;
    private int hit;
    
    //public GameObject projectileExplosion;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
        
        wind = GameObject.FindGameObjectWithTag("Environment");
        currentMap = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Tilemap>();
        AudioSource.PlayClipAtPoint(spawnAudio, this.transform.position);
        hit = 1;
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
        if (gameObject.tag == "Reflective" && hit > 0)
        {
            hit = 0;
            AudioSource.PlayClipAtPoint(spawnAudio, collision.transform.position);
        }
        if (collision.gameObject.layer == 9)
        {
            AudioSource.PlayClipAtPoint(hitEnemyAudio, this.transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitGroundAudio, this.transform.position);
        }
        if (!cannon.onShot)
        {
            Vector2 finalDirection = rb2d.velocity.normalized;
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
        }        
    }
}
