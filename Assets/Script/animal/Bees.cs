using System;
using Script.tile;
using UnityEngine;

namespace Script.animal
{
    public class Bees : MonoBehaviour
    {
        public Action<PlantableTile> OnAnimationFinished { get; set; }
        
        private GameObject _activePlantableTile;

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