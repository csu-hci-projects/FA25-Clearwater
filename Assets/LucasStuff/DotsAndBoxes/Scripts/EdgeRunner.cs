using UnityEngine;

namespace DotsAndBoxes
{
    [RequireComponent(typeof(Renderer))]
    public class EdgeRunner : MonoBehaviour
    {
        public EdgeType Type;
        public int X;
        public int Y;
        private Controller master;
        private readonly float Transparency = 0.5f;
        private Material Mat;

        void Start()
        {
            Mat = GetComponent<Renderer>().material;
            Mat.SetFloat("_Surface", 1f);
            Mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            Mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            Mat.SetInt("_ZWrite", 0);
            Mat.DisableKeyword("_ALPHATEST_ON");
            Mat.EnableKeyword("_ALPHABLEND_ON");
            Mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            Mat.renderQueue = 3000;
            Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, 0.0f);
        }

        private void OnMouseEnter()
        {
            Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, Transparency);
        }

        private void OnMouseExit()
        {
            Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, 0.0f);
        }
    }
}
