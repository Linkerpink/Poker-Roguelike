using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotate : MonoBehaviour
{
    [SerializeField] private float m_speed;

    private void Update()
    {
        transform.Rotate(new Vector3(0,0, m_speed * Time.deltaTime));
    } 
}
