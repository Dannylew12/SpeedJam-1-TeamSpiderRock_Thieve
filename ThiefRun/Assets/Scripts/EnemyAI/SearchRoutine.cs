using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRoutine : MonoBehaviour
{

    private EnemyBehavior searcher;
    private float sqrSchRge, cosSchAgl;
    private void Start()
    {
        searcher = GetComponent<EnemyBehavior>();
        sqrSchRge = searchRange * searchRange;
        cosSchAgl = Mathf.Cos(searchAngle * Mathf.PI / 180f);
    }

    [SerializeField] private float searchRange;
    [SerializeField] private float searchAngle;
    void Update()
    {
        /// if we are searching for player
        if (searcher.curStatus != EnemyBehavior.STATUS.CHASE && searcher.curStatus != EnemyBehavior.STATUS.DEFAULT)
        {
            Vector3 enemyHead = transform.position + new Vector3(0, 1, 0);
            Vector3 toPlayer = searcher.player.position - enemyHead;
            /// if player is close enough and is in the search angle
            if (toPlayer.sqrMagnitude < sqrSchRge &&
                Vector3.Dot(toPlayer, transform.forward) / toPlayer.magnitude >= cosSchAgl)
            {
                /// AND there's nothing between the enemy face and middle of player
                /// then set the behavior to CHASE
                RaycastHit hit;
                if (Physics.Raycast(enemyHead, toPlayer, out hit) && hit.collider.tag == "Player")
                {
                    searcher.curStatus = EnemyBehavior.STATUS.CHASE;
                }
            }
        }
    }

}
