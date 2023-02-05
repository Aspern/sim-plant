using System;
using Script.common;
using Script.plant;
using Script.tile;
using UnityEngine;

namespace Script
{
    public class Environment : HighlightManager
    {
        public const string EnvironmentComponentName = "Environment";
        
        private GameObject _selectedTile;
        private HudController _controller;

        private void Awake()
        {
            _controller = GameObject.Find(HudController.HudControllerComponentName).GetComponent<HudController>();
        }

        public void SelectionChange(GameObject tileGameObj)
        {
            ChangeHighlightEffect(tileGameObj);
            var plantableTile = tileGameObj.GetComponent<PlantableTile>();
            if (plantableTile)
            {
                // Remove old listeners from further selection.
                plantableTile.OnAnimationFinished = null;
            }
            _selectedTile = tileGameObj;
        }

        public void ExecuteNectarActionForSelection()
        {
            var plantableTile = _selectedTile.GetComponent<PlantableTile>();

            if (!plantableTile || !plantableTile.IsPlanted()) return;
            _controller.ActionMenu.DisableNectarButton();
            plantableTile.Plant.GetComponent<Plant>().Bloom();
        }
    }
}