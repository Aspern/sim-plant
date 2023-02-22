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
        
        private GameObject _plantPrefab;
        public Action<PlantableTile> OnAnimationFinished { get; set; }
        public GameObject Plant { get; private set; }
        public bool BeesPresent { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _plantPrefab = Resources.Load<GameObject>("Prefabs/plant");
        }

        protected override void Start()
        {
            base.Start();

            if (!startPlanted) return;
            Planting();
        }

        public bool IsPlanted()
        {
            return Plant != null;
        }

        public void Planting()
        {
            if (IsPlanted()) return;
            
            var parentTransform = transform;
            var position = parentTransform.position;
            var plant = Instantiate(
                _plantPrefab,
                position,
                Quaternion.identity,
                parentTransform
            );
            Plant = plant;
            plant.GetComponent<Plant>().OnAnimationFinished = () =>
            {
                OnAnimationFinished?.Invoke(this);
            };
        }

        public void DestroyPlant()
        {
            Destroy(Plant);
            Plant = null;
            OnAnimationFinished?.Invoke(this);
        }
    }
}