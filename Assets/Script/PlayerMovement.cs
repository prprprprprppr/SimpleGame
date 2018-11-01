using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private float MoveAcceleration;
    [SerializeField]
    private float RotateSpeed;
    [SerializeField]
    private float JumpForce;

    private PlayerGetInput _playerInput;
    private Animator anim;
    private Rigidbody rbody;

    private Vector3 currentVelocity;
    private bool onGround = false;
    private bool Run = false;

    private List<Collider> _collisions = new List<Collider>();


    //碰撞判断是否在地面上
    private void OnCollisionEnter(Collision collision)
    {
        var contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0f)
            {
                if (!_collisions.Contains(collision.collider))
                    _collisions.Add(collision.collider);
                onGround = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0f)
            {
                validSurfaceNormal = true;
                break;
            }
        }

        if (validSurfaceNormal)
        {
            onGround = true;
            if (!_collisions.Contains(collision.collider))
            {
                _collisions.Add(collision.collider);
            }
        }
        else
        {
            if (_collisions.Contains(collision.collider))
            {
                _collisions.Remove(collision.collider);
            }
            if (_collisions.Count == 0)
            {
                onGround = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_collisions.Contains(collision.collider))
        {
            _collisions.Remove(collision.collider);
        }
        if (_collisions.Count == 0) { onGround = false; }
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rbody = GetComponentInChildren<Rigidbody>();
        _playerInput = GetComponent<PlayerGetInput>();
    }

    private void Update()
    {
        if (onGround)
            Move();
        JumpingAndLanding();
    }

    private void Move()
    {
        anim.SetFloat("InputH", _playerInput.InputHorizontal);
        anim.SetFloat("InputV", _playerInput.InputVertical);

        if (_playerInput.InputLeftShift && _playerInput.InputHorizontal == 0 && _playerInput.InputVertical > 0)
        {
            Run = true;
            Camera.main.GetComponent<CameraEffect_MotionBlur2>().setMotionBlurEN(true);
        }
        else
        {
            Run = false;
            Camera.main.GetComponent<CameraEffect_MotionBlur2>().setMotionBlurEN(false);
        }
        anim.SetBool("Run", Run);

        Vector3 MoveInput = getMoveDirection(_playerInput.InputHorizontal, _playerInput.InputVertical);
        Vector3 targetvelocity = MoveInput * Time.deltaTime * MoveSpeed * (Run ? 3 : 1);
        rbody.velocity = Vector3.MoveTowards(rbody.velocity, targetvelocity, MoveAcceleration * Time.deltaTime);

        anim.SetFloat("Rotate", _playerInput.InputRotate);
        transform.Rotate(transform.up * _playerInput.InputRotate * RotateSpeed * Time.deltaTime);
    }

    private Vector3 getMoveDirection(float Inputx, float Inputz)
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 MoveDir = Inputx * right + Inputz * forward;
        MoveDir.Normalize();
        return MoveDir;
    }

    private void JumpingAndLanding()
    {
        if (onGround && _playerInput.InputJump)
        {
            anim.SetTrigger("Jump");
            rbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
            anim.SetBool("Land", false);
        }
        else if (onGround)
        {
            anim.SetBool("Land", true);
        }
    }

    //不加会报错..
    public void Hit()
    {
    }

    public void Shoot()
    {
    }

    public void FootR()
    {
    }

    public void FootL()
    {
    }

    public void Jump()
    {
    }

    public void Land()
    {
    }
}
