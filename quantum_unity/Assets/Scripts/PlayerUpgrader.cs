using System;
using Quantum;
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
        
        _subscription = QuantumEvent.Subscribe<EventPlayerUpgraded>(this, x =>
        {
            PlayerDataReactive.Value = x.data;
        });
    }

    public void Destroyed(QuantumGame game)
    {
        UpgradeButton.UpgradeClicked -= UpgradeRandom;
        QuantumEvent.Unsubscribe(_subscription);
    }
    
    private void UpgradeRandom()
    {
        var game = QuantumRunner.Default.Game;
        var frame = game.Frames.Verified;

        if (!TryGetLocalPlayer(out int playerId))
            return;

        if (frame.TryGet(view.EntityRef, out PlayerData playerData))
             playerData.StartUpgrade();
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
