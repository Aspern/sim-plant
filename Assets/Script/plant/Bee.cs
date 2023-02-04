using UnityEngine;

public class Bee : MonoBehaviour
{
    public float initialRotateTime = 30f;
    private void Start()
    {
        LeanTween.rotateAround(gameObject, new Vector3(0, 1, 0), 360, initialRotateTime).setOnComplete(o =>
        {
            gameObject.GetComponentInParent<Tile>().OnPollinated();
            gameObject.GetComponentInParent<TreePlant>().Pollinated = true;
            gameObject.GetComponentInParent<TreePlant>().RemoveBees();
        }); 
    }
}