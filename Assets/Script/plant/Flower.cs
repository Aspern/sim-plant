using UnityEngine;

public class Flower : MonoBehaviour
{
    public float initialGrowTime = 20f;

    private void Awake()
    {
        transform.localScale = new Vector3(0.5f, 1, 0.5f);
    }

    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), initialGrowTime).setOnComplete(o =>
        {
            gameObject.GetComponentInParent<Tile>().OnFlourished();
        });
    }
}