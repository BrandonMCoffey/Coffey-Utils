using GameEventSystems.Logic;
using UnityEngine;
using UnityEngine.Events;

namespace GameEventSystems
{
    public class ActionOnEvent : GameEventListener
    {
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        public override void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}