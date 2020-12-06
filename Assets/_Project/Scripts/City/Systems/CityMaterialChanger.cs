using System;
using UnityEngine;

namespace _Project.Scripts.City.Systems
{
    [Serializable]
    public class CityMaterialChanger
    {
        private const float FullVisibilityAlpha = 1.0f;

        [SerializeField]
        private float _transparentAlpha = 0.5f;

        [SerializeField]
        private Material[] _buildingsMaterials;

        private void ChangeBuildingsVisibility(float alpha)
        {
            foreach (var material in _buildingsMaterials)
            {
                var color = material.color;
                var newColor = new Color(color.r, color.g, color.b, alpha);

                material.SetColor("_BaseColor", newColor);
            }
        }

        public void MakeBuildingsVisible()
        {
            ChangeBuildingsVisibility(FullVisibilityAlpha);
        }

        public void MakeBuildingsTransparent()
        {
            ChangeBuildingsVisibility(_transparentAlpha);
        }
    }
}