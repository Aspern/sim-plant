using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private List<Material> _materials;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    
    private void Awake()
    {
        _materials = new List<Material>();
        foreach (var rend in GetComponents<Renderer>())
        {
            _materials.AddRange(new List<Material>(rend.materials));
        }
    } 

    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach (var material in _materials)
            {
                material.EnableKeyword("_EMISSION");
                material.SetVector(EmissionColor,  new Vector4(1f,0f,0f, 1f) * 0.1f);
            }
        }
        else
        {
            foreach (var material in _materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}