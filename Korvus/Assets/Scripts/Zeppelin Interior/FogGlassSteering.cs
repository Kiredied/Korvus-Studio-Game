using System.Collections.Generic;
using UnityEngine;

public class FogGlassSteering : MonoBehaviour
{
    [Tooltip("Drag every glass renderer here. Leave empty to find them automatically under this object.")]
    [SerializeField] private List<Renderer> glassRenderers = new List<Renderer>();

    [Tooltip("Include inactive glass objects when auto-finding.")]
    [SerializeField] private bool includeInactive = true;

    [Tooltip("How fast the fog moves per unit of steering speed.")]
    [SerializeField] private float flowScale = 0.15f;

    [Tooltip("Flip these to reverse the fog direction on each axis.")]
    [SerializeField] private Vector2 axisSigns = new Vector2(1f, -1f);

    private static readonly int ScrollOffsetID = Shader.PropertyToID("_ScrollOffset");

    private readonly List<Material> glassMaterials = new List<Material>();
    private Vector2 offset;
    private Vector2 targetFlow;

    void Awake()
    {
        if (glassRenderers.Count == 0)
        {
            glassRenderers.AddRange(GetComponentsInChildren<Renderer>(includeInactive));
        }

        foreach (var r in glassRenderers)
        {
            if (r == null) continue;
            // .material creates a per-object instance, so the shared asset isn't changed
            glassMaterials.Add(r.material);
        }
    }

    public void SetFlow(float forward, float sideward)
    {
        targetFlow = new Vector2(sideward * axisSigns.x, forward * axisSigns.y);
    }

    void Update()
    {
        offset += targetFlow * flowScale * Time.deltaTime;
        offset.x = Mathf.Repeat(offset.x, 2f);
        offset.y = Mathf.Repeat(offset.y, 2f);

        foreach (var mat in glassMaterials)
        {
            mat.SetVector(ScrollOffsetID, offset);
        }
    }
}