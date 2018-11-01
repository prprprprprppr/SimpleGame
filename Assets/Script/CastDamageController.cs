using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastDamageController : MonoBehaviour {

    [SerializeField]
    private float tranform_Offset;
    [SerializeField]
    private float AttackAngle;
    [SerializeField]
    private int AttackRaycastNum;
    [SerializeField]
    private float AttackRaycastDistance;

    [HideInInspector]
    public int Animator_AttackLayer;

    private Animator anim;
    private string anim_name;
    private bool Attacking = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Attack(int ActionIndex)
    {
        if (Attacking && !anim.GetCurrentAnimatorStateInfo(Animator_AttackLayer).IsName(anim_name))
        {
            Attacking = false;
        }
        if (!Attacking && ActionIndex!=0)
        {
            switch (ActionIndex)
            {
                case 1:
                    PlayAttackAnimator("Unarmed-Attack-Kick-L1");
                    break;
                case 2:
                    PlayAttackAnimator("Unarmed-Attack-Kick-R1");
                    break;
                case 3:
                    PlayAttackAnimator("Unarmed-Attack-L2");
                    break;
                case 4:
                    PlayAttackAnimator("Unarmed-Attack-R1");
                    break;
                case 5:
                    PlayAttackAnimator("Unarmed-Attack-L3");
                    break;
                case 6:
                    PlayAttackAnimator("Unarmed-Attack-R2");
                    break;
                case 7:
                    PlayAttackAnimator("Unarmed-Attack-L1");
                    break;
                case 8:
                    PlayAttackAnimator("Unarmed-Attack-R3");
                    break;
                default: break;
            }
            AttackRangeDetection();
            ActionIndex = 0;
        }
    }

    private void PlayAttackAnimator(string s)
    {
        Attacking = true;
        anim_name = s;
        anim.Play(s, Animator_AttackLayer, 0);
    }

    void AttackRangeDetection()
    {
        RaycastHit _hit;

        Vector3 forward = transform.forward;
        if (Physics.Raycast(transform.position + new Vector3(0, tranform_Offset, 0), forward, out _hit))
        {
            CastAttackDamage(_hit);
        }
        else
        {
            float AttackPerAngle = AttackAngle / AttackRaycastNum;
            for (int i = 1; i <= AttackRaycastNum; i++)
            {
                Vector3 dir = Quaternion.Euler(0, AttackPerAngle * i, 0) * forward;
                if (Physics.Raycast(transform.position + new Vector3(0, tranform_Offset, 0), dir, out _hit))
                {
                    CastAttackDamage(_hit);
                    break;
                }

                dir = Quaternion.Euler(0, -AttackPerAngle * i, 0) * forward;
                if (Physics.Raycast(transform.position + new Vector3(0, tranform_Offset, 0), dir, out _hit))
                {
                    CastAttackDamage(_hit);
                    break;
                }
            }
        }
        //Draw Ray
        {
            Vector3 dir = Quaternion.Euler(0, AttackAngle, 0) * forward;
            Debug.DrawRay(transform.position + new Vector3(0, tranform_Offset, 0), dir * 10, Color.red);
            dir = Quaternion.Euler(0, -AttackAngle, 0) * forward;
            Debug.DrawRay(transform.position + new Vector3(0, tranform_Offset, 0), dir * 10, Color.red);
        }
    }

    void CastAttackDamage(RaycastHit _hit)
    {
        if (_hit.distance < AttackRaycastDistance)
        {
            Debug.DrawLine(transform.position + new Vector3(0, tranform_Offset, 0), _hit.point, Color.red);
            var takedamage = _hit.collider.gameObject.GetComponent<TakeDamageController>();
            if (takedamage != null)
            {
                int num = Random.Range(10, 20);
                takedamage.GetHurt(num,_hit.point);
            }
        }
    }
}
