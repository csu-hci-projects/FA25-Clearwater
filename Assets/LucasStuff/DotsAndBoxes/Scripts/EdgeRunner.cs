using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EdgeRunner : MonoBehaviour
{
    protected int X;
    protected int Y;
    private readonly float transparency = 0.5f;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetFloat("_Surface", 1f);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0.0f);
    }

    private void OnMouseEnter()
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, transparency);
    }

    private void OnMouseExit()
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0.0f);
    }
}
