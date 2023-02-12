using Script.hud;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public const string HudControllerComponentName = "HUD";
    public ActionMenu ActionMenu { get; private set; }
    public Compass Compass { get; private set; }
    
    public PlantCounter PlantCounter { get; set; }

    private void Awake()
    {
        ActionMenu = GameObject.Find(ActionMenu.ActionMenuComponentName).GetComponent<ActionMenu>();
        Compass = GameObject.Find(Compass.CompassComponentName).GetComponent<Compass>();
        PlantCounter = GameObject.Find(PlantCounter.PlantCounterComponentName).GetComponent<PlantCounter>();
    }
}
