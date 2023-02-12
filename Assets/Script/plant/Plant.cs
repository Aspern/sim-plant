using System;
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
        
        [Header("Environment")]
        [Tooltip("Time in seconds a plant needs to grow.")]
        public float growDuration = 3f;
        
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
    }
}