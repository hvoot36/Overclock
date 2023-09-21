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

    public GameObject origin;

    private float timeBetweenShots;
    public float startTimeBetweenShots;
    public int minLoudnessValue;


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
                
                if (Mathf.RoundToInt(detector.loudnessValue * 1000) > 40)
                {
                    GameObject currentProjectile = Instantiate(projectile, projectileOrigin.position, transform.rotation);
                    
                    currentProjectile.transform.parent = null;
                    currentProjectile.GetComponent<Projectile>().speed *= ((Mathf.RoundToInt(detector.loudnessValue * 100) - minLoudnessValue) / (100 - minLoudnessValue) + 1) * 0.8f;
                    timeBetweenShots = startTimeBetweenShots;
                }
            }
            else
            {
                
                projectileSpriteRenderer.flipX = true;
                
                if (Mathf.RoundToInt(detector.loudnessValue * 1000) > 30)
                {
                    GameObject currentProjectile = Instantiate(projectile, projectileOrigin.position, transform.rotation);

                    currentProjectile.transform.parent = null;
                    currentProjectile.GetComponent<Projectile>().speed *= ((Mathf.RoundToInt(detector.loudnessValue * 100) - minLoudnessValue) / (100 - minLoudnessValue) + 1) * 0.8f;
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
