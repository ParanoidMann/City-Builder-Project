using System;
using ModestTree;
using UnityEngine;

namespace _Project.Scripts.Interaction.Input
{
    public abstract class AInputController : MonoBehaviour
    {
        protected event Action<Vector3Int> _clickDownEvent;
        protected event Action _cancelClickEvent;

        protected abstract Vector3Int? RaycastGround();

        protected void InvokeClickDownAfterRaycast()
        {
            var position = RaycastGround();
            if (position != null)
            {
                _clickDownEvent?.Invoke(position.Value);
            }
        }

        protected void InvokeCancelClick()
        {
            _cancelClickEvent?.Invoke();
        }

        #region SUBSCRIBE_METHODS

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
        
        public void SubscribeCancelClick(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            _cancelClickEvent += action;
        }

        public void UnsubscribeCancelClick(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            _cancelClickEvent -= action;
        }
        
        #endregion
    }
}