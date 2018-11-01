using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBag : MonoBehaviour {

    public int HP;

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 100, Space.World);
    }
}
