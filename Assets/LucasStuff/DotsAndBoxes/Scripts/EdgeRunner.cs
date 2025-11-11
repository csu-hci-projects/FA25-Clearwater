using System;
using UnityEngine;

namespace DotsAndBoxes
{
    [RequireComponent(typeof(Renderer))]
    public class EdgeRunner : MonoBehaviour
    {
        [SerializeField] EdgeType Type;
        public Edge edge;
        private Controller master;
        private static readonly float Transparency = 0.5f;
        private Material Mat;
        private bool isSet;

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

            isSet = false;
        }

        public void Init(Controller controller, int x, int y)
        {
            master = controller;
            edge = new(Type, x, y);
        }

        public void AISet()
        {
            if (!isSet)
            {
                Mat.color = new Color(1, 0, 0, 1);
                isSet = true;
            }
            else
            {
                throw new Exception("AI tried to set an already-set edge!");
            }
        }

        private void OnMouseEnter()
        {
            if (!isSet)
                Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, Transparency);
        }

        private void OnMouseDown()
        {
            if (!isSet)
            {
                bool successful = master.TryMove(edge, Player.Human);
                if (successful)
                {
                    Mat.color = new Color(0, 0, 1, 1);
                    isSet = true;
                }
            }
        }

        private void OnMouseExit()
        {
            if (!isSet)
                Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, 0);
        }
    }
}
