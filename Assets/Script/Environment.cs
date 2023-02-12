using System.Collections.Generic;
using Script.animal;
using Script.common;
using Script.plant;
using Script.tile;
using UnityEngine;

namespace Script
{
    public class Environment : HighlightManager
    {
        public const string EnvironmentComponentName = "Environment";

        [Header("Environment")]
        [Tooltip("Time in seconds where environment updates game objects like bees etc.")]
        public float updatePeriod = 3.0f;

        [Tooltip("Number of bees that should be spawned on the map.")]
        public int numberOfBees = 1;

        [Tooltip("The 3D Model of the bee object.")]
        public GameObject beePrefab;

        private GameObject _selectedTile;
        private HudController _controller;
        private TileMap _map;
        private float _nextActionTime;
        private readonly List<GameObject> _bees = new();

        private void Awake()
        {
            _controller = GameObject.Find(HudController.HudControllerComponentName).GetComponent<HudController>();
            _map = GameObject.Find(EnvironmentComponentName).GetComponent<TileMap>();
        }


        protected override void Start()
        {
            base.Start();
            CreateBees();
        }

        private void Update()
        {
            if (!(Time.time > _nextActionTime)) return;

            _nextActionTime += updatePeriod;
            UpdateEnvironment();
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

        public void ExecuteBeeActionForSelection()
        {
            var plantableTile = _selectedTile.GetComponent<PlantableTile>();

            if (!plantableTile || !plantableTile.IsPlanted()
                               || !plantableTile.Plant.GetComponent<Plant>().IsBloomed()) return;
            
            _controller.ActionMenu.DisableBeeButton();
            plantableTile.Plant.GetComponent<Plant>().GrowBud();
        }
        
        
        public void ExecuteSeedActionForSelection()
        {
            var plantableTile = _selectedTile.GetComponent<PlantableTile>();

            if (!plantableTile || !plantableTile.IsPlanted()
                               || !plantableTile.Plant.GetComponent<Plant>().IsBudGrown()) return;
            
            _controller.ActionMenu.DisableSeedButton();
            var neighbors = _map.Neighbors(_selectedTile.transform.position);
            var direction = _controller.Compass.CurrentDirection;
            var target = neighbors[direction];
            
            plantableTile.Plant.GetComponent<Plant>().Seed(target);
        }

        private void UpdateEnvironment()
        {
            UpdateBeePositions();
            UpdatePlantCounter();
        }

        private void UpdateBeePositions()
        {
            _bees.ForEach(bee =>
            {
                var bees = bee.GetComponent<Bees>();
                var target = _map.RandomPollinableTile();

                if (target == null)
                {
                    target = _map.RandomPlantableTile();
                }

                bees.Move(target);
            });
        }

        private void UpdatePlantCounter()
        {
            var numPlantableTiles = _map.PlantableTiles().Count;
            var numPlantedTiles = _map.PlantedTiles().Count;
            
            _controller.PlantCounter.SetCounter(numPlantedTiles, numPlantableTiles);
        }

        private void CreateBees()
        {
            for (var i = 0; i < numberOfBees; i++)
            {
                var randomPlantableTile = _map.RandomPlantableTile();
                var bee = Instantiate(
                    beePrefab,
                    randomPlantableTile.transform.position,
                    Quaternion.identity,
                    transform
                );

                bee.GetComponent<Bees>().OnAnimationFinished ??= (plantableTile) =>
                {
                    if (_selectedTile && _selectedTile.GetComponent<PlantableTile>() == plantableTile)
                    {
                        _controller.ActionMenu.CheckActionButtonsInteractable(plantableTile);
                    }
                };

                _bees.Add(bee);
            }
        }
    }
}