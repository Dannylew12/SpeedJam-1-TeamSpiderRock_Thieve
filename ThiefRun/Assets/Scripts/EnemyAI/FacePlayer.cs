using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private EnemyBehavior enemy;
    private Transform player;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemy = GetComponentInParent<EnemyBehavior>();

        if (enemy == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    [SerializeField] private bool singleSprite = false;
    private void Update()
    {
        if (!singleSprite)
        {
            transform.LookAt(enemy.player);
            SetAnimator();
        }
        else transform.LookAt(player);
    }

    [SerializeField] private Sprite[] faceSprites;
    private const float cos45 = 1.4142f / 2.0f; /// sqrt(2)/2
    private void SetSprite()
    {
        float dirForwVal = Vector3.Dot(enemy.transform.forward, (enemy.player.position - enemy.transform.position).normalized);
        float dirRghtVal = Vector3.Dot(enemy.transform.right, (enemy.player.position - enemy.transform.position).normalized);

        if (dirForwVal >  cos45)
            spriteRenderer.sprite = faceSprites[0];
        if (dirForwVal < -cos45)
            spriteRenderer.sprite = faceSprites[1];
        if (dirRghtVal >  cos45)
            spriteRenderer.sprite = faceSprites[2];
        if (dirRghtVal < -cos45)
            spriteRenderer.sprite = faceSprites[3];
    }

    private EnemyBehavior.STATUS prevStatus;
    private void SetAnimator()
    {
        if (enemy.curStatus == EnemyBehavior.STATUS.IDLE || enemy.curStatus == EnemyBehavior.STATUS.DEFAULT)
        {
            float dirForwVal = Vector3.Dot(enemy.transform.forward, (enemy.player.position - enemy.transform.position).normalized);
            float dirRghtVal = Vector3.Dot(enemy.transform.right, (enemy.player.position - enemy.transform.position).normalized);

            if (dirForwVal > cos45)
                animator.SetInteger("viewFrom", 0);
            if (dirForwVal < -cos45)
                animator.SetInteger("viewFrom", 1);
            if (dirRghtVal > cos45)
                animator.SetInteger("viewFrom", 2);
            if (dirRghtVal < -cos45)
                animator.SetInteger("viewFrom", 3);
        }
        else if (enemy.curStatus == EnemyBehavior.STATUS.CHASE && prevStatus != enemy.curStatus)
        {
            animator.SetInteger("viewFrom", -1);
            animator.SetTrigger("chasing");
        }
        else if (enemy.curStatus == EnemyBehavior.STATUS.PATROL && prevStatus != enemy.curStatus)
        {
            animator.SetInteger("viewFrom", -1);
            animator.SetTrigger("walk");
        }
        prevStatus = enemy.curStatus;
    }

}
