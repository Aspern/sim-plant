using System;
using Script.tile;
using UnityEngine;

namespace Script.plant
{
    /**
     * <summary>
     * Describes a general plant which can bloom, create a bud and seed into a neighbour area to reproduce itself.
     *
     * If a plant seeds into an other area, it automatically starts to wither. During the withering process the plant
     * can be saved by re-creating a bloom. If the wither process has finished, the plant is withered and needs to be
     * destroyed.
     * </summary>
     */
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
        [Tooltip("Time in seconds a plant needs wither.")]
        public float witherDuration = 5f;

        public Action OnAnimationFinished { get; set; }
        public bool IsBlooming { get; private set; }
        public bool IsBudGrowing { get; private set; }
        public bool IsWithered { get; private set; }
        
        private GameObject Flower { get; set; }
        private GameObject Bud { get; set; }
        private LTDescr _witherAnimation;

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

            StopWithering();

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
                if (target.GetComponent<PlantableTile>() && !target.GetComponent<PlantableTile>().IsPlanted())
                {
                    target.GetComponent<PlantableTile>().Planting();
                }
                Wither();
                OnAnimationFinished?.Invoke();
            });
        }

        private void Wither()
        {
            _witherAnimation = LeanTween.color(gameObject, new Color(0.63f, 0.32f, 0.18f), witherDuration)
                .setOnComplete(() =>
                {
                    IsWithered = true;
                    OnAnimationFinished?.Invoke();
                });
        }

        private void StopWithering()
        {
            if (_witherAnimation == null) return;

            LeanTween.cancel(gameObject, _witherAnimation.uniqueId);
            LeanTween.color(gameObject, Color.white, 0);
        }
    }
}