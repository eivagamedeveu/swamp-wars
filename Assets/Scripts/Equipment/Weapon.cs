using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    [SerializeField] private float _attackRange;
    [SerializeField] private WeaponType _type;

    public float AttackRange => _attackRange;

    public void Init()
    {
        _attackRange = _type switch
        {
            WeaponType.Short => LocalDataManager.Instance.Config.ShortWeaponRange,
            WeaponType.Medium => LocalDataManager.Instance.Config.MediumWeaponRange,
            WeaponType.Long => LocalDataManager.Instance.Config.LongWeaponRange,
            WeaponType.Range => LocalDataManager.Instance.Config.RangeWeaponRange,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum WeaponType
{
    Short,
    Medium,
    Long,
    Range
}
