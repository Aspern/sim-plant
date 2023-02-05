using System;
using UnityEngine;

namespace Script.plant
{
    public class Plant : MonoBehaviour
    {

        [Header("General")]
        [Tooltip("3D Model of the plants flower.")]
        public GameObject flowerPrefab;
        
        [Header("Environment")]
        [Tooltip("Time in seconds a plant needs to grow.")]
        public float growDuration = 3f;
        
        public Action OnAnimationFinished { get; set; }
        public GameObject Flower { get; private set; }
        public bool IsBlooming { get; private set; }
        
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
    }
}