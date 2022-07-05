using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]

public class UseZone : MonoBehaviour
{
    public IUsable Target { get; private set; }
    public event UnityAction<bool> IsTargetChanged; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IUsable target)) return;
        
        Target = target;
        IsTargetChanged?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IUsable target)) return;
        
        Target = null;
        IsTargetChanged?.Invoke(false);
    }
}
