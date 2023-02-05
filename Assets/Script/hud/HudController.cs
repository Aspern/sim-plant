using Script.hud;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public const string HudControllerComponentName = "HUD";
    public ActionMenu ActionMenu { get; set; }

    private void Awake()
    {
        ActionMenu = GameObject.Find(ActionMenu.ActionMenuComponentName).GetComponent<ActionMenu>();
    }
}
