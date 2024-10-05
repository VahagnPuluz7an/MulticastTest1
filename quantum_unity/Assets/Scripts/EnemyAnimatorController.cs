using Quantum;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private EntityView view;
    
    private static readonly int TakingDamage = Animator.StringToHash("TakingDamage");

    private DispatcherSubscription _subscription;
    private int _enabledPlayer;
    
    private void OnEnable()
    {
        _subscription = QuantumEvent.Subscribe(this, (EventEnemyTakingDamage tackingDamage) => TakeDamageAnim(tackingDamage.Entity,tackingDamage.active, tackingDamage.player));
    }

    private void OnDisable()
    {
        QuantumEvent.Unsubscribe(_subscription);
    }

    private void TakeDamageAnim(EntityRef entity, bool takingDamage, PlayerRef player)
    {
        if (view.EntityRef != entity)
            return;

        if (takingDamage)
            _enabledPlayer = player;

        if (player != _enabledPlayer)
            return;
        
        if (!takingDamage)
            _enabledPlayer = -1;
        anim.SetBool(TakingDamage, takingDamage);
    }
}