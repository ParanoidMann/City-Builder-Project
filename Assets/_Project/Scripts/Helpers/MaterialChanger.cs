using UnityEngine;

namespace _Project.Scripts.Helpers
{
    public static class MaterialChanger
    {
        public static void ChangeAlpha(Material material, float alpha)
        {
            var color = material.color;
            var newColor = new Color(color.r, color.g, color.b, alpha);
            
            material.SetColor("_Color", newColor);
        }
    }
}