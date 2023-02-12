using System;
using Script.tile;
using UnityEngine;

namespace Script.plant
{
    public class Plant : MonoBehaviour
    {

        [Header("General")]
        [Tooltip("3D Model of the plants flower.")]
        public GameObject flowerPrefab;
        
        [Tooltip("3D Model of the plants bud.")]
        public GameObject budPrefab;
        
        [Tooltip("3D Model of the plants sed animation.")]
        public GameObject seedPrefab;
        
        [Header("Environment")]
        [Tooltip("Time in seconds a plant needs to grow.")]
        public float growDuration = 3f;
        
        [Tooltip("Time in seconds a the seed needs to enter a neighbour field.")]
        public float seedAnimationDuration = 2f;
        
        public Action OnAnimationFinished { get; set; }
        public bool IsBlooming { get; private set; }
        public bool IsBudGrowing  { get; private set; }

        private GameObject Flower { get; set; }
        private GameObject Bud { get; set; }
        
        private void Awake()
        {
            transform.localScale = new Vector3(0, 0.5f, 0);
        }
        
        private void Start()
        {
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), growDuration).setOnComplete(() =>
            {
                OnAnimationFinished?.Invoke();
            });
        }
        
        public bool IsBloomed()
        {
            return Flower != null;
        }

        public bool IsBudGrown()
        {
            return Bud != null;
        }

        public void Bloom()
        {
            if (IsBloomed()) return;
            IsBlooming = true;
            
            var parentTransform = transform;
            var position = parentTransform.position;
            var flower = Instantiate(
                flowerPrefab,
                position,
                Quaternion.identity,
                parentTransform
            );
            flower.GetComponent<Flower>().OnAnimationFinished = () =>
            {
                Flower = flower;
                IsBlooming = false;
                OnAnimationFinished?.Invoke();
            };
        }

        public void GrowBud()
        {
            if (IsBudGrown()) return;
            IsBudGrowing = true;
            Destroy(Flower);
            Flower = null;
            
            var parentTransform = transform;
            var position = parentTransform.position;
            var bud = Instantiate(
                budPrefab,
                position,
                Quaternion.identity,
                parentTransform
            );
            bud.GetComponent<Bud>().OnAnimationFinished = () =>
            {
                Bud = bud;
                IsBudGrowing = false;
                OnAnimationFinished?.Invoke();
            };
        }

        public void Seed(GameObject target)
        {
            var parentTransform = transform;
            var position = parentTransform.position;
            var seed = Instantiate(
                seedPrefab,
                position,
                Quaternion.identity,
                parentTransform
            );
            
            LeanTween.move(seed, target.transform.position, seedAnimationDuration).setOnComplete(() =>
            {
                Destroy(Bud);
                Bud = null;
                Destroy(seed);
                target.GetComponent<PlantableTile>().Planting();
                OnAnimationFinished?.Invoke();
            });
        }
    }
}