using UnityEngine;

namespace Pooling
{
    public class AutoDisabler : MonoBehaviour
    {
        [SerializeField] private float time;

        private bool _enabled;
        private float _timer;

        private void OnEnable()
        {
            _timer = 0;
            _enabled = true;
        }

        private void Update()
        {
            if (!_enabled)
                return;

            _timer += Time.deltaTime;

            if(_timer >= time)
            {
                _timer = 0;
                _enabled = false;

                gameObject.SetActive(false);
            }
        }
    }
}