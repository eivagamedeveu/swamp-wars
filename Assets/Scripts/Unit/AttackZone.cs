using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class AttackZone : MonoBehaviour
{
    public List<IDamageable> Targets { get; private set; } = new List<IDamageable>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable target)) return;
        
        Targets.Add(target);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable target)) return;
        
        Targets.Remove(target);
    }

    public void SetAttackZone(float size)
    {
        var localScale = transform.localScale;
        localScale = new Vector3(size, localScale.y, localScale.z);
        
        var localPosition = transform.localPosition;
        localPosition = new Vector3(2 + size, localPosition.y, localPosition.z);
        
        transform.localScale = localScale;
        transform.localPosition = localPosition;
    }
}
