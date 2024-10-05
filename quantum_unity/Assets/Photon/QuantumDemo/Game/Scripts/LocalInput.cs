using Photon.Deterministic;
using Quantum;
using UnityEngine;

public class LocalInput : MonoBehaviour
{
  private void OnEnable()
  {
    QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
  }

  public void PollInput(CallbackPollInput callback)
  {
    var input = new Quantum.Input();
    float x = UnityEngine.Input.GetAxis("Horizontal");
    float y = UnityEngine.Input.GetAxis("Vertical");

    input.Direction = new Vector2(x, y).ToFPVector2();

    callback.SetInput(input, DeterministicInputFlags.Repeatable);
  }
}
