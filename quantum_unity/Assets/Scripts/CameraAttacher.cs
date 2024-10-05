using Cinemachine;
using Quantum;
using UnityEngine;

public class CameraAttacher : MonoBehaviour
{
    [SerializeField] private EntityView view;
    
    public void OnInit(QuantumGame game)
    {
        var frame = game.Frames.Verified;

        if (!frame.TryGet(view.EntityRef, out PlayerLink link)) return;
        if (!game.PlayerIsLocal(link.Player)) return;
        
        var virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = transform;
        virtualCamera.LookAt = transform;
    }
}
