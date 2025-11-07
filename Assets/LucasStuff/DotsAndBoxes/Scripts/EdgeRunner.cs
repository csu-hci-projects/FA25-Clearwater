using UnityEngine;

namespace DotsAndBoxes
{
    [RequireComponent(typeof(Renderer))]
    public class EdgeRunner : MonoBehaviour
    {
        [SerializeField] EdgeType Type;
        public Edge edge;
        private Controller master;
        private readonly float Transparency = 0.5f;
        private Material Mat;

        void Awake()
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

        public void Init(Controller controller, int x, int y)
        {
            master = controller;
            edge = new(Type, x, y);
        }

        private void OnMouseEnter()
        {
            Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, Transparency);
        }

        private void OnMouseDown()
        {
            master.TryMove(edge);
        }

        private void OnMouseExit()
        {
            Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, 0.0f);
        }
    }
}
