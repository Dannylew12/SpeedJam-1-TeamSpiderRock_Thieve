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
        
        /// Set cone of vision to correct size
        Light visionCone = transform.GetChild(1).GetComponent<Light>();
        if (visionCone != null)
        {
            visionCone.range = searchRange;
            visionCone.spotAngle = searchAngle;
        }
    }

    [SerializeField] private float searchRange;
    [SerializeField] private float searchAngle;
    void Update()
    {
        /// if we are searching for player
        if (searcher.curStatus != EnemyBehavior.STATUS.CHASE && searcher.curStatus != EnemyBehavior.STATUS.DEFAULT)
        {
            Vector3 enemyHead = transform.position + new Vector3(0, .5f, 0);
            Vector3 toPlayer = searcher.player.position - enemyHead;
            /// if player is close enough and is in the search angle
            if (toPlayer.sqrMagnitude < sqrSchRge &&
                Vector3.Dot(toPlayer, transform.forward) / toPlayer.magnitude >= cosSchAgl)
            {
                /// AND there's nothing between the enemy face and middle of player
                /// then set the behavior to CHASE
                RaycastHit[] hits = Physics.RaycastAll(enemyHead, toPlayer);
                float minDistance = 1000f;
                string closestTarget = "";
                foreach (RaycastHit hit in hits)
                {
                    if (hit.distance < minDistance)
                    {
                        minDistance = hit.distance;
                        closestTarget = hit.collider.tag;
                    }
                }
                if (closestTarget == "Player")
                {
                    searcher.curStatus = EnemyBehavior.STATUS.CHASE;
                    GameObject.FindGameObjectWithTag("MainTheme").GetComponent<Alarmed>().PlayAlarmed();
                }
            }
        }
    }

}
