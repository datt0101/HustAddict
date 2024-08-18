using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/LightingProfile", order = 1)]
public class LightingProfile : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}
