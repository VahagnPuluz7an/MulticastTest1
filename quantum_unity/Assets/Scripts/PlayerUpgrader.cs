using System;
using Quantum;
using Quantum.Player;
using UniRx;
using UnityEngine;

public class PlayerUpgrader : MonoBehaviour
{
    public static event Action<PlayerUpgrader> LocalPlayerInited;
    
    [SerializeField] private EntityView view;

    public ReactiveProperty<PlayerData> PlayerDataReactive { get; private set; } = new ReactiveProperty<PlayerData>();

    private DispatcherSubscription _subscription;
    
    public void Inited(QuantumGame game)
    {
        UpgradeButton.UpgradeClicked += UpgradeRandom;
        
        if (!TryGetLocalPlayer(out int id)) return;
        
        LocalPlayerInited?.Invoke(this);
        
        var frame = game.Frames.Verified;
        if (frame.TryGet(view.EntityRef, out PlayerData playerData))
            PlayerDataReactive.Value = playerData;

        _subscription = QuantumEvent.Subscribe<EventPlayerUpgraded>(this, OnUpgrade);
    }

    public void Destroyed(QuantumGame game)
    {
        UpgradeButton.UpgradeClicked -= UpgradeRandom;
        QuantumEvent.Unsubscribe(_subscription);
    }
    
    private void UpgradeRandom()
    {
        if (!TryGetLocalPlayer(out int playerId))
            return;

        var command = new PlayerUpgradeCommand()
        {
            PlayerEntity = view.EntityRef,
        };
        QuantumRunner.Default.Game.SendCommand(playerId,command);
    }
    
    private void OnUpgrade(EventPlayerUpgraded callback)
    {
        if (callback.Entity == view.EntityRef)
            PlayerDataReactive.Value = callback.data;
    }
    
    private bool TryGetLocalPlayer(out int player)
    {
        player = -1;
        var game = QuantumRunner.Default.Game;
        var frame = game.Frames.Verified;
        if(!frame.TryGet(view.EntityRef, out PlayerLink link))
            return false;
        if (!game.PlayerIsLocal(link.Player))
            return false;
        player = link.Player;
        return true;
    }
}
