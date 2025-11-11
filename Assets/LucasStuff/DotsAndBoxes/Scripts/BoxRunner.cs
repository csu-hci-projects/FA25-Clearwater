using UnityEngine;

namespace DotsAndBoxes
{
    [RequireComponent(typeof(Renderer))]
    public class BoxRunner : MonoBehaviour
    {
        private Material Mat;

        void Awake()
        {
            Mat = GetComponent<Renderer>().material;
            Mat.SetFloat("_Surface", 1);
            Mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            Mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            Mat.SetInt("_ZWrite", 0);
            Mat.DisableKeyword("_ALPHATEST_ON");
            Mat.EnableKeyword("_ALPHABLEND_ON");
            Mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            Mat.renderQueue = 3000;
            Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, 0);
        }

        public void Set(Player player)
        {
            Mat.color = player == Player.Human ? new Color(0, 0, 1, 1) : new Color(1, 0, 0, 1);
        }
    }
}
