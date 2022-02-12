using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresh : MonoBehaviour
{
    public Vector3 movementSpeed;
    void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime);
    }
}
