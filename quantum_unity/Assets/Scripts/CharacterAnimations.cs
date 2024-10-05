using Quantum;
using UnityEngine;

public class CharacterAnimations : QuantumCallbacks
{
    private EntityView _entityView;
    private Animator _animator;
    
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _entityView = GetComponent<EntityView>();
        _animator = GetComponentInChildren<Animator>();
    }

    public override void OnUpdateView(QuantumGame game)
    {
        var frame = game.Frames.Predicted;
        var controller = frame.Get<CharacterController3D>(_entityView.EntityRef);
        _animator.SetFloat(Speed, controller.Velocity.Magnitude.AsFloat);
    }
}
