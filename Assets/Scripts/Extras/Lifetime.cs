using UnityEngine;
using UnityEngine.Events;

public class Lifetime : MonoBehaviour
{
    [SerializeField]
    private UnityEvent triggeredEvent;

    [SerializeField]
    private float lifetime = 1;

    private void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;
        if(lifetime <= 0)
        {
            triggeredEvent?.Invoke();
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
