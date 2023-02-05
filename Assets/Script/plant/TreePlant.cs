using UnityEngine;

public class TreePlant : MonoBehaviour
{
    public float initialGrowTime = 60f;
    public float initialDieTime = 60f;
    public GameObject flowerPrefab;
    public GameObject beePrefab;
    public bool Pollinated { get; set; }


    private GameObject _flowerGameObj;
    private LTDescr _dieAction;

    private void Awake()
    {
        transform.localScale = new Vector3(0, 0.5f, 0);
    }

    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), initialGrowTime).setOnComplete(o =>
        {
            gameObject.GetComponentInParent<Tile>().OnFullyGrown();
        });
    }

    public void CreateFlower()
    {
        if (_flowerGameObj) return;

        var parentTransform = transform;
        _flowerGameObj = Instantiate(
            flowerPrefab,
            parentTransform.position,
            Quaternion.identity,
            parentTransform
        );

        if (_dieAction == null) return;
        LeanTween.cancel(gameObject, _dieAction.uniqueId);
        LeanTween.color(gameObject, Color.white, 0);

    }

    public void RemoveFlower()
    {
        if (!_flowerGameObj) return;
        Destroy(_flowerGameObj);

        _dieAction = LeanTween.color(gameObject, new Color(0.63f, 0.32f, 0.18f), initialDieTime).setOnComplete(() =>
        {
            gameObject.GetComponentInParent<Tile>().DryPlant();
        });
    }

    public void UseBees()
    {
        if (gameObject.GetComponentInParent<Tile>().BeeGameObj)
        {
            
        }
    }
}