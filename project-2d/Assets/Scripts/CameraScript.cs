using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject John;

    void Update()
    {
        if (John != null)
        {
            Vector3 position = transform.position;
            position.x = John.transform.position.x;
            transform.position = position;
        }
    }
}
