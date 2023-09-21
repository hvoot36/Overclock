using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float movementForce = 10f;


    private float movementX;
    private float movementY;

    public SpriteRenderer sr;
    public Animator animator;

    public GameObject shotOrigin;
    public Vector3 invertedPosition = new Vector3(-0.654f, -0.136f, 0.1017688f);
    private Vector3 rightPosition = new Vector3(0.654f, -0.136f, 0.1017688f);

    private string WALK_ANIMATION = "walk";
    private string DEATH_ANIMATION = "dead";


    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        animatePlayer();
    }

    void playerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(movementX) > Mathf.Abs(movementY))
        {
            movementY = 0;
        }
        else
        {
            movementX = 0;
        }
        transform.position += movementForce * Time.deltaTime * new Vector3(movementX, movementY, 0);
    }

    void animatePlayer()
    {
        if (movementX > 0 || movementY > 0)
        {
            Debug.Log(rightPosition);
            animator.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
            shotOrigin.transform.position = transform.position + rightPosition;
            Debug.Log(rightPosition);
        }
        else if (movementX < 0 || movementY < 0)
        {
            
            animator.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
            shotOrigin.transform.position = transform.position + invertedPosition;
        }
        else
        {
            animator.SetBool(WALK_ANIMATION, false);
        }
    }

    public void LoseCondition()
    {
        animator.SetBool(DEATH_ANIMATION, true);
        StartCoroutine(destroyObj());
    }

    IEnumerator destroyObj()
    {
        WaitForSeconds wait = new(2f);
        while (true)
        {
            yield return wait;
            Destroy(gameObject); break;
        }
    }
}
