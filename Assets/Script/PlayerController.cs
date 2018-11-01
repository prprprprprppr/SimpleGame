using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public SimpleHPBar hpbar;

    [SerializeField]
    private int AttackLayer;
    [SerializeField]
    private int DamageLayer;

    private PlayerGetInput _input;
    private PlayerMovement movement;
    private CastDamageController castdamage;
    private TakeDamageController takedamage;

    private void OnCollisionEnter(Collision collision)
    {
        var comp = collision.gameObject.GetComponent<BloodBag>();
        if (comp != null)
        {
            takedamage.AddSubHP(comp.HP);
            Destroy(collision.gameObject);
        }
    }

    private void Awake()
    {
        _input = GetComponent<PlayerGetInput>();
        movement = GetComponent<PlayerMovement>();
        castdamage = GetComponent<CastDamageController>();
        takedamage = GetComponent<TakeDamageController>();
        castdamage.Animator_AttackLayer = AttackLayer;
        takedamage.Animator_DamageLayer = DamageLayer;
        takedamage.HpBar = hpbar;
        takedamage.HpBar.Offset = 160;
    }

    private void Update()
    {
        if (takedamage.CharactorAlive)
            castdamage.Attack(_input.ActionIndex);
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        _input.enabled = false;
        movement.enabled = false;
        this.enabled = false;
        GameObject.FindWithTag("ScoreUI").GetComponent<ScoreText>().GameOver();
    }

}
