using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 10f;
    public float lifeTime = 0.15f;
    public LayerMask isSolid;
    // Start is called before the first frame update

    private SpriteRenderer sr;

    public float distance;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        lifeTime = 0.15f;
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        


        if (sr.flipX)
        {
            RaycastHit2D hitInformation = Physics2D.Raycast(transform.position, transform.right, distance);
            if (hitInformation.collider != null)
            {
                Debug.Log(hitInformation);
                if (hitInformation.collider.CompareTag("Enemy"))
                {
                    Destroy(hitInformation.collider.gameObject);
                }

                DestroyProjectile();
            }
            transform.Translate(speed * Time.deltaTime * Vector2.left);

        }
        else
        {
            RaycastHit2D hitInformation = Physics2D.Raycast(transform.position, transform.right, distance);
            if(hitInformation.collider != null)
            {
                Debug.Log(hitInformation);
                if(hitInformation.collider.CompareTag("Enemy"))
                {
                    Destroy(hitInformation.collider.gameObject);
                }

                DestroyProjectile();
            }

            transform.Translate(speed * Time.deltaTime * Vector2.right);
        }
        
        
    }

    void DestroyProjectile() { Destroy(gameObject); }
}
