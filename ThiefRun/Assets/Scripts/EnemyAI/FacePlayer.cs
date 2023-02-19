using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private EnemyBehavior enemy;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy = GetComponentInParent<EnemyBehavior>();
    }

    private void Update()
    {
        transform.LookAt(enemy.player);
        SetSprite();
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
}
