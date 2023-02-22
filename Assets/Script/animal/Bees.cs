using System;
using Script.tile;
using UnityEngine;

namespace Script.animal
{
    public class Bees : MonoBehaviour
    {
        [Header("Environment")]
        [Tooltip("Time in seconds where bees try to move.")]
        public float updatePeriod = 3.0f;
        
        public Action<PlantableTile> OnAnimationFinished { get; set; }
        
        private float _nextActionTime;
        private GameObject _activePlantableTile;
        private TileMap _map;

        private void Awake()
        {
            _map = GameObject.Find(Environment.EnvironmentComponentName).GetComponent<TileMap>();
        }

        private void Update()
        {
            if (!(Time.time > _nextActionTime)) return;

            _nextActionTime += updatePeriod;

            UpdatePosition();
        }
        
        private void UpdatePosition()
        {
            var target = _map.RandomPollinableTile();

            if (target == null)
            {
                target = _map.RandomPlantableTile();
            }

            Move(target);
        }

        public void Move(GameObject target)
        {
            if (_activePlantableTile)
            {
                _activePlantableTile.GetComponent<PlantableTile>().BeesPresent = false;
            }
            
            LeanTween.move(gameObject, target.transform.position, 1.0f).setOnComplete(() =>
            {
                _activePlantableTile = target;
                var plantableTile = _activePlantableTile.GetComponent<PlantableTile>();
                plantableTile.BeesPresent = true;
                
                OnAnimationFinished?.Invoke(plantableTile);
            });
            
        }
    }
}