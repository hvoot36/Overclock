using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius;
    [SerializeField]
    private float angle = 15;

    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject targetGameObject;

    public bool CanSeePlayer { get; private set; }

    void Start()
    {
        targetGameObject = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new(0.2f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0 )
        {
            UnityEngine.Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            if (Vector2.Angle(transform.right * -1, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    CanSeePlayer = true;
                    Debug.Log("Can see player.");
                    targetGameObject.gameObject.GetComponent<Player>().LoseCondition();
                }
                    
                else
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else if(CanSeePlayer)
        {
            CanSeePlayer = false;
        }
    }
    


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angle02 = DirectionFromAngle(transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetGameObject.transform.position);
        }
    }
    #endif


private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void enemyDestroy()
    {
        gameObject.GetComponent<Animator>().SetBool("death", true);
        StartCoroutine(destroyEnemy());
    }

    IEnumerator destroyEnemy()
    {
        WaitForSeconds wait = new(1f);
        while (true)
        {
            yield return wait;
            Destroy(gameObject);
        }
    }



}
