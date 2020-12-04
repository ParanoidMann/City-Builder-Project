using System;
using ModestTree;
using UnityEngine;

namespace _Project.Scripts.Interaction.Input
{
    public abstract class AInputController : MonoBehaviour
    {
        protected event Action<Vector3Int> _clickDownEvent;

        protected abstract Vector3Int? RaycastGround();

        protected void TryInvokeClickDown()
        {
            var position = RaycastGround();
            if (position != null)
            {
                _clickDownEvent?.Invoke(position.Value);
            }
        }

        public void SubscribeClickDown(Action<Vector3Int> action)
        {
            Assert.IsNotNull(action, "Action is null");
            _clickDownEvent += action;
        }

        public void UnsubscribeClickDown(Action<Vector3Int> action)
        {
            Assert.IsNotNull(action, "Action is null");
            _clickDownEvent -= action;
        }
    }
}