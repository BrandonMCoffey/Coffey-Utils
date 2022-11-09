using UnityEngine;

namespace GameEventSystems.Logic
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _event = null;

        private bool _missingEvent;

        private void Awake()
        {
            if (_event == null) {
                _missingEvent = true;
                throw new MissingComponentException("Missing Game Event reference on " + gameObject);
            }
        }

        private void OnEnable()
        {
            if (_missingEvent) return;
            _event.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (_missingEvent) return;
            _event.UnRegisterListener(this);
        }

        public virtual void OnEventRaised()
        {
        }
    }
}