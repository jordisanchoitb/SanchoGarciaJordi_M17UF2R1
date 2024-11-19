using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AEntity
{
    private PlayerControlers pControlers;
    private void Awake()
    {
        pControlers = GetComponent<PlayerControlers>();

    }


}
