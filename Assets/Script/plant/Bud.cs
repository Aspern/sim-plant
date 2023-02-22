using System;
using UnityEngine;

namespace Script.plant
{
    public class Bud : MonoBehaviour
    {
        [Header("Environment")]
        [Tooltip("Time in seconds a bud needs to grow.")]
        public float growDuration = 3f;

        public Action OnAnimationFinished { get; set; }
        
        private void Awake()
        {
            transform.localScale = new Vector3(0.5f, 1, 0.5f);
        }

        private void Start()
        {
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), growDuration).setOnComplete(_ =>
            {
                OnAnimationFinished();
            });
        }
    }
}