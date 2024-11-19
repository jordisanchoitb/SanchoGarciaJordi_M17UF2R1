using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    protected int Hp { get; set; }
    protected int Atk { get; set; }
    protected int CountCoints { get; set; }
    protected int CountKeys { get; set; }
    protected AWeaponSO Weapon { get; set; }

}
