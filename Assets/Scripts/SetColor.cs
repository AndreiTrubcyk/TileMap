using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{

    private Color _availableColor = Color.green;
    private Color _unavailableColor = Color.red;
    private List<Material> _materials = new();
    
    private void Awake()
    {
        var renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in renderers)
        {
            _materials.Add(meshRenderer.material);
        }
    }

    public void SetRightColor(bool isAvailable)
    {
        if (isAvailable)
        {
            foreach (var material in _materials)
            {
                material.color = _availableColor;
            }
        }
        else
        {
            foreach (var material in _materials)
            {
                material.color = _unavailableColor;
            }
        }
    }

    public void ResetColor()
    {
        foreach (var material in _materials)
        {
            material.color = Color.white;
        }
    }
}
