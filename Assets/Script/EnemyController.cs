using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    [HideInInspector]
    public bool canAttack = false;

    [SerializeField]
    private int AttackLayer;
    [SerializeField]
    private int DamageLayer;

    private CastDamageController castdamage;
    private TakeDamageController takedamage;
    private EnemyMovement movement;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        castdamage = GetComponent<CastDamageController>();
        takedamage = GetComponent<TakeDamageController>();
        castdamage.Animator_AttackLayer = AttackLayer;
        takedamage.Animator_DamageLayer = DamageLayer;
        takedamage.HpBar = GetComponentInChildren<SimpleHPBar>();
        takedamage.HpBar.Offset = 1;
    }

    private void Update()
    {
        if (takedamage.CharactorAlive)
        {
            if (canAttack)
            {
                castdamage.Attack(Random.Range(1, 9));
            }
        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        movement.enabled = false;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        this.enabled = false;
        GameObject.FindWithTag("ScoreUI").GetComponent<ScoreText>().AddScore();
        Destroy(gameObject, 5);
    }

}
