using UnityEngine;

namespace _Project.Scripts.Helpers
{
    public static class GameObjectUtils
    {
        private static void SetChildrenActive(GameObject gameObject, bool isActive)
        {
            for (var i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(isActive);
            }
        }

        public static void ShowChildren(this GameObject gameObject)
        {
            SetChildrenActive(gameObject, true);
        }

        public static void HideChildren(this GameObject gameObject)
        {
            SetChildrenActive(gameObject, false);
        }
    }
}