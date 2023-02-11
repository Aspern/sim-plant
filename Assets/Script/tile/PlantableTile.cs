using System;
using Script.plant;
using UnityEngine;

namespace Script.tile
{

    public class PlantableTile : Tile
    {
        [Header("Environment")] 
        [Tooltip("If set to true the tile creates a plant at the beginning of the game.")]
        public bool startPlanted;

        [Tooltip("The 3D Model of the plant used for this tile.")]
        public GameObject plantPrefab;

        public Action<PlantableTile> OnAnimationFinished { get; set; }
        public GameObject Plant { get; private set; }

        protected override void Start()
        {
            base.Start();

            if (startPlanted)
            {
                Planting();
            }
        }

        public bool IsPlanted()
        {
            return Plant != null;
        }

        private void Planting()
        {
            if (IsPlanted()) return;
            
            var parentTransform = transform;
            var position = parentTransform.position;
            var plant = Instantiate(
                plantPrefab,
                position,
                Quaternion.identity,
                parentTransform
            );
            plant.GetComponent<Plant>().OnAnimationFinished = () =>
            {
                Plant = plant;
                OnAnimationFinished?.Invoke(this);
            };
        }
    }
}