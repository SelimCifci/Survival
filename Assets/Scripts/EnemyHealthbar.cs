using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
    public Transform mainCamera;
    public SpriteRenderer bar;

    private void Update()
    {
        transform.LookAt(mainCamera);

        bar.size = new Vector2(0.016f*GetComponentInParent<Enemy>().hp, bar.size.y);
    }
}
