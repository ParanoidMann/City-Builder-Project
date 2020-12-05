using System;
using ModestTree;
using UnityEngine;

namespace _Project.Scripts.Interaction.Input
{
    public abstract class AInputController : MonoBehaviour
    {
        protected event Action MoveUpEvent;
        protected event Action MoveDownEvent;
        protected event Action MoveRightEvent;
        protected event Action MoveLeftEvent;

        protected event Action ZoomInEvent;
        protected event Action ZoomOutEvent;

        protected event Action CancelClickEvent;
        protected event Action<Vector3Int> ClickDownEvent;

        protected abstract Vector3Int? RaycastGround();

        #region INVOKE_METHODS

        protected void InvokeMoveUp()
        {
            MoveUpEvent?.Invoke();
        }

        protected void InvokeMoveDown()
        {
            MoveDownEvent?.Invoke();
        }

        protected void InvokeMoveRight()
        {
            MoveRightEvent?.Invoke();
        }

        protected void InvokeMoveLeft()
        {
            MoveLeftEvent?.Invoke();
        }

        protected void InvokeZoomIn()
        {
            ZoomInEvent?.Invoke();
        }

        protected void InvokeZoomOut()
        {
            ZoomOutEvent?.Invoke();
        }

        protected void InvokeCancelClick()
        {
            CancelClickEvent?.Invoke();
        }

        protected void InvokeClickDownAfterRaycast()
        {
            var position = RaycastGround();
            if (position != null)
            {
                ClickDownEvent?.Invoke(position.Value);
            }
        }

        #endregion

        #region SUBSCRIBE_METHODS

        public void SubscribeMoveUp(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            MoveUpEvent += action;
        }

        public void UnsubscribeMoveUp(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            MoveUpEvent -= action;
        }

        public void SubscribeMoveDown(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            MoveDownEvent += action;
        }

        public void UnsubscribeMoveDown(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            MoveDownEvent -= action;
        }

        public void SubscribeMoveRight(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            MoveRightEvent += action;
        }

        public void UnsubscribeMoveRight(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            MoveRightEvent -= action;
        }

        public void SubscribeMoveLeft(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            MoveLeftEvent += action;
        }

        public void UnsubscribeMoveLeft(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            MoveLeftEvent -= action;
        }

        public void SubscribeZoomIn(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            ZoomInEvent += action;
        }

        public void UnsubscribeZoomIn(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            ZoomInEvent -= action;
        }

        public void SubscribeZoomOut(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            ZoomOutEvent += action;
        }

        public void UnsubscribeZoomOut(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            ZoomOutEvent -= action;
        }

        public void SubscribeCancelClick(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            CancelClickEvent += action;
        }

        public void UnsubscribeCancelClick(Action action)
        {
            Assert.IsNotNull(action, "Action is null");
            CancelClickEvent -= action;
        }

        public void SubscribeClickDown(Action<Vector3Int> action)
        {
            Assert.IsNotNull(action, "Action is null");
            ClickDownEvent += action;
        }

        public void UnsubscribeClickDown(Action<Vector3Int> action)
        {
            Assert.IsNotNull(action, "Action is null");
            ClickDownEvent -= action;
        }

        #endregion
    }
}