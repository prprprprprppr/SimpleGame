using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageController : MonoBehaviour {

    [SerializeField]
    private int MaxHp = 100;

    [HideInInspector]
    public bool CharactorAlive = true;
    [HideInInspector]
    public int Animator_DamageLayer;
    [HideInInspector]
    public SimpleHPBar HpBar;

    private Animator anim;
    private bool Hurting = false;
    private string anim_name;
    private int CurHp;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        FloatingPopTextController.Initialize();
        CurHp = MaxHp;
    }

    public void GetHurt(int DamageNum,Vector3 point)
    {
        if (!CharactorAlive)
            return;
        AddSubHP(-DamageNum);
        TakeDamageAnimator();
        TakeDamageText(DamageNum, point);
    }

    private void TakeDamageAnimator()
    {

        if (Hurting && !anim.GetCurrentAnimatorStateInfo(Animator_DamageLayer).IsName(anim_name))
        {
            Hurting = false;
        }
        if (!Hurting)
        {
            int s = Random.Range(0, 5);
            switch (s)
            {
                case 0:PlayAnimation("Unarmed-GetHit-B1");break;
                case 1:PlayAnimation("Unarmed-GetHit-F1");break;
                case 2:PlayAnimation("Unarmed-GetHit-F2");break;
                case 3:PlayAnimation("Unarmed-GetHit-L1");break;
                case 4:PlayAnimation("Unarmed-GetHit-R1");break;
                default:break;
            }
        }
    }

    private void PlayAnimation(string s)
    {
        anim_name = s;
        Hurting = true;
        anim.Play(s, Animator_DamageLayer, 0);
    }

    private void TakeDamageText(int DamageNum, Vector3 point)
    {
        FloatingPopTextController.CreateFloatingText(DamageNum.ToString(), point);
    }

    public int GetHP()
    {
        return CurHp;
    }
    public void AddSubHP(int num)
    {
        CurHp += num;
        if (CurHp > MaxHp) CurHp = MaxHp;
        if (CurHp < 0)
        {
            CurHp = 0;
            CharactorAlive = false;
            anim.Play("Unarmed-Death1", 0, 0);
        }
        HpBar.Per = (float)CurHp / MaxHp;
    }
}
