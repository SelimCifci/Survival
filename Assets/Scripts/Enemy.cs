using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;

    public int hp;
    public float speed;
    public int damage;
    public float attackSpeed;

    bool attack = false;
    int attemptedAttacks;
    int initializedAttacks;
    bool canAttack = true;

    float distance;

    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        CollisionDetection();

        anim.SetFloat("distance", distance);
        anim.SetBool("attack", attack);

        var rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        rotation.x = 0; rotation.z = 0;
        transform.rotation = rotation;

        if (hp <= 0) { Destroy(gameObject); }

        if (!attack)
        {
            Vector3 move = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            if (distance <= 10) { Vector3.MoveTowards(transform.position, player.transform.position, speed * 2 * Time.deltaTime); }
            
            transform.position = new Vector3(move.x, transform.position.y, move.z);
        }
        if (attack && canAttack)
        {
            StartCoroutine(Hit());
            canAttack = false;
        }
        print(attemptedAttacks);
        print(initializedAttacks);
    }

    private IEnumerator Hit()
    {
        initializedAttacks = attemptedAttacks;
        yield return new WaitForSeconds(attackSpeed);
        if (attack && attemptedAttacks == initializedAttacks)
        {
            player.GetComponent<Player>().hp -= damage;
            yield return new WaitForSeconds(1.333f-attackSpeed);
        }
        canAttack = true;
    }

    private void CollisionDetection()
    {
        if (distance <= 1.5f && !attack) { attack = true; attemptedAttacks++; }
        else if (distance > 1.5f) { attack = false; }
    }
}
