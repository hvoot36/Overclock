using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioDetection detector;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer projectileSpriteRenderer;

    [SerializeField]
    private bool lookingRight = true;



    public GameObject projectile;
    public Transform projectileOrigin;

    private float timeBetweenShots;
    public float startTimeBetweenShots;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        projectileSpriteRenderer = projectile.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.flipX)
            lookingRight = false;
        else lookingRight = true;

        attack();
    }

    void attack()
    {
        if(timeBetweenShots <= 0)
        {
            if (lookingRight)
            {
                projectileSpriteRenderer.flipX = false;
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(projectile, projectileOrigin.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShots;
                }
            }
            else
            {
                projectileSpriteRenderer.flipX = true;
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(projectile, projectileOrigin.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShots;
                }
            }
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
        
        
    }
}
