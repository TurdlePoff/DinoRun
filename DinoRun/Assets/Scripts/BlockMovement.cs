﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float m_fMovementSpeed = -0.05f;

    private Vector3 m_vMovementAddition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_vMovementAddition = new Vector3(m_fMovementSpeed, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + m_vMovementAddition * Time.deltaTime;
    }
}
