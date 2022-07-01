using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected float _weight;

    public string Name => _name;
    public float Weight => _weight;
}
