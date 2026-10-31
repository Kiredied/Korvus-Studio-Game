using UnityEngine;

public class DamageHitRelay : MonoBehaviour
{
    [SerializeField] private IndividualDamage owner;

    private void OnTriggerEnter(Collider other)
    {
        if (owner != null)
        {
            owner.OnHammerTrigger(other);
        }
    }
}