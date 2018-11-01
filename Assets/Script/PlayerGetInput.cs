using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetInput : MonoBehaviour {
    
    [HideInInspector]
    public float InputHorizontal;
    [HideInInspector]
    public float InputVertical;
    [HideInInspector]
    public float InputRotate;
    [HideInInspector]
    public bool InputJump;
    [HideInInspector]
    public bool InputLeftShift;
    [HideInInspector]
    public int ActionIndex;

    private bool InputAttackKick;
    private bool InputAttackUp;
    private bool InputAttackStright;
    private bool InputAttackDown;

    private void Update()
    {
        getInputs();
    }

    private void getInputs()
    {
        InputHorizontal = Input.GetAxis("Horizontal");
        InputVertical = Input.GetAxis("Vertical");
        InputRotate = Input.GetAxis("Rotate");
        InputJump = Input.GetButtonDown("Jump");
        InputLeftShift = Input.GetKey(KeyCode.LeftShift);
        InputAttackKick = Input.GetKeyDown(KeyCode.I);
        InputAttackUp = Input.GetKeyDown(KeyCode.J);
        InputAttackStright = Input.GetKeyDown(KeyCode.K);
        InputAttackDown = Input.GetKeyDown(KeyCode.L);

        if(InputAttackKick || InputAttackUp|| InputAttackStright|| InputAttackDown)
        {
            int r = Random.Range(0, 2);
            if (InputAttackKick)
                ActionIndex = (r == 0) ? 1 : 2;
            else if (InputAttackUp)
                ActionIndex = (r == 0) ? 3 : 4;
            else if (InputAttackStright)
                ActionIndex = (r == 0) ? 5 : 6;
            else
                ActionIndex = (r == 0) ? 7 : 8;
        }
        else
        {
            ActionIndex = 0;
        }
    }
}
