using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Barrel",menuName ="Barrel")]
public class Barrel : ScriptableObject
{
    [SerializeField] float RelaodSpeed;
    [SerializeField] int Damage;
    [SerializeField] int Cost;

    public float relaodSpeed => relaodSpeed;
    public int damage => Damage;
    public int cost => Cost;


}
