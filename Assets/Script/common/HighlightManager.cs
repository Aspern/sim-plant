using JetBrains.Annotations;
using UnityEngine;

namespace Script.common
{
    public class HighlightManager : MonoBehaviour
    {
        [Header("General")]
        [Tooltip("3D model for the highlight effect if a tile gets selected.")]
        public GameObject selectionHighlightPrefab;

        private GameObject _selectionHighlight;

        protected virtual void Start()
        {
            CreateSelectHighlightObj();
        }


        protected void ChangeHighlightEffect(GameObject target)
        {
            if (!target)
            {
                _selectionHighlight.SetActive(false);
            }
            else
            {
                _selectionHighlight.SetActive(true);
                _selectionHighlight.transform.position = target.transform.position;
            }
        }

        private void CreateSelectHighlightObj()
        {
            var parentTransform = transform;
            var position = parentTransform.position;
            _selectionHighlight = Instantiate(
                selectionHighlightPrefab,
                position,
                Quaternion.identity,
                parentTransform
            );
            _selectionHighlight.SetActive(false);
        }
    }
}