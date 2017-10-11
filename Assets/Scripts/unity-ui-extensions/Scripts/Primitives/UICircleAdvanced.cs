/// Credit zge
/// Sourced from - http://forum.unity3d.com/threads/draw-circles-or-primitives-on-the-new-ui-canvas.272488/#post-2293224

using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/Primitives/UI Circle Advanced")]
    public class UICircleAdvanced : MaskableGraphic
    {
        [SerializeField]
        Texture m_Texture;
        //public float radius = 1;
        [Range(0, 360)]
        public int fillPercent = 360;
        public bool fill = true;
        public float thickness = 5;
        [Range(-100, 100)]
        public float xOffset = 0;
        [Range(-100, 100)]
        public float yOffset = 0;
        [Range(0, 360)]
        public int segments = 360;
     
        public override Texture mainTexture
        {
            get
            {
                return m_Texture == null ? s_WhiteTexture : m_Texture;
            }
        }
     
     
        /// <summary>
        /// Texture to be used.
        /// </summary>
        public Texture texture
        {
            get
            {
                return m_Texture;
            }
            set
            {
                if (m_Texture == value)
                    return;
     
                m_Texture = value;
                SetVerticesDirty();
                SetMaterialDirty();
            }
        }
     
     
        void Update()
        {
            this.thickness = (float)Mathf.Clamp(this.thickness, 0, rectTransform.rect.width / 2);
        }
     
        protected UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
        {
            UIVertex[] vbo = new UIVertex[4];
            for (int i = 0; i < vertices.Length; i++)
            {
                var vert = UIVertex.simpleVert;
                vert.color = color;
                vert.position = vertices[i];
                vert.uv0 = uvs[i];
                vbo[i] = vert;
            }
            return vbo;
        }
     
     
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            float outer = rectTransform.pivot.x * rectTransform.rect.width;
            float inner = rectTransform.pivot.x * rectTransform.rect.width - this.thickness;
     
            vh.Clear();

            Vector2 prevX = new Vector2(xOffset, yOffset);
            Vector2 prevY = new Vector2(xOffset, yOffset);
            //Vector2 prevX = Vector2.zero;
            //Vector2 prevY = Vector2.zero;
            Vector2 uv0 = new Vector2(0, 0);
            Vector2 uv1 = new Vector2(0, 1);
            Vector2 uv2 = new Vector2(1, 1);
            Vector2 uv3 = new Vector2(1, 0);
            Vector2 pos0;
            Vector2 pos1;
            Vector2 pos2;
            Vector2 pos3;

            //Determine start position and end position to have the right angle
            float yOffset2 = Mathf.Pow(yOffset, 2);
            float xOffset2 = Mathf.Pow(xOffset, 2);
            float r2 = Mathf.Pow(outer, 2);
            float newX = Mathf.Sqrt((r2 - yOffset2) / r2) * outer;
            float newY = Mathf.Sqrt((r2 - xOffset2) / r2) * outer;
            Vector2 startPos = new Vector2(xOffset, newY);
            Vector2 endPos = new Vector2(newX, yOffset);
            int counter = 0;

            //bool isEnd = false;

            float f = (this.fillPercent / 360f);
            float degrees = 360f / segments;
            int fa = (int)(segments * f) ;
     
     
            for (int i = 0; i <= fa; i++)
            {
                float rad = Mathf.Deg2Rad * (i * degrees);
                float c = Mathf.Cos(rad);   // X
                float s = Mathf.Sin(rad);   // Y
     
                uv0 = new Vector2(0, 1);
                uv1 = new Vector2(1, 1);
                uv2 = new Vector2(1, 0);
                uv3 = new Vector2(0, 0);

                if (outer * c < xOffset)
                {          
                    if (counter == 0)
                    {
                        pos0 = prevX;
                        pos1 = new Vector2(xOffset, outer * s);
                        pos2 = new Vector2(xOffset, yOffset);
                        pos3 = new Vector2(xOffset, yOffset);
                        vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
                    }
                    counter++; 
                    continue;
                }

                if (outer * s < yOffset)
                {
                    prevX = endPos;
                    continue;
                }

                //if (outer * s < yOffset)
                //{
                //    //    prevX = new Vector2(newX, yOffset);
                //    isEnd = true;
                //}
                // Draw Outer vertices

                //if (outer * c < prevX[0])
                //{
                //    //continue;
                //    float yOffset2 = Mathf.Pow(yOffset, 2);
                //    float r2 = Mathf.Pow(outer, 2);
                //    float newX = Mathf.Sqrt((r2 - yOffset2) / r2) * outer;
                //    pos0 = prevX;
                //    pos1 = new Vector2(newX, yOffset);
                //}
                //else if (outer * s > prevX[1])
                //{
                //    //continue;
                //    float xOffset2 = Mathf.Pow(xOffset, 2);
                //    float r2 = Mathf.Pow(outer, 2);
                //    float newY = Mathf.Sqrt((r2 - xOffset2) / r2) * outer;
                //    pos0 = prevX;
                //    pos1 = new Vector2(xOffset, newY);
                //}

                pos0 = prevX;
                pos1 = new Vector2(outer * c, outer * s);

                //else
                //{
                //    //float xOffset2 = Mathf.Pow(xOffset, 2);
                //    //float r2 = Mathf.Pow(outer, 2);
                //    //float newY = Mathf.Sqrt((r2 - xOffset2) / r2) * outer;
                //    //pos0 = new Vector2(xOffset, newY);
                //    //pos1 = prevY;
                //    pos0 = prevX;
                //    pos1 = new Vector2(outer * c, outer * s);
                //}

                // Draw origin     
                if (fill)
                {
                    
                    //else if (Mathf.Abs(inner * c) > Mathf.Abs(prevX[0]))
                    //{
                    //    float yOffset2 = Mathf.Pow(yOffset, 2);
                    //    float r2 = Mathf.Pow(outer, 2);
                    //    float newX = Mathf.Sqrt((r2 - yOffset2) / r2) * outer;
                    //    pos2 = new Vector2(newX, inner * s);
                    //    pos3 = prevY;
                    //}
                    pos2 = new Vector2(xOffset, yOffset);
                    pos3 = new Vector2(xOffset, yOffset);
                    //pos2 = Vector2.zero;
                    //pos3 = Vector2.zero;
                }
                else
                {
                    pos2 = new Vector2(inner * c, inner * s);
                    pos3 = prevY;
                }
               
                prevX = pos1;
                prevY = pos2;

                vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
     
            }
        }
    }
}



//BACKUP!!!
/// Credit zge
/// Sourced from - http://forum.unity3d.com/threads/draw-circles-or-primitives-on-the-new-ui-canvas.272488/#post-2293224

//using System.Collections.Generic;

//namespace UnityEngine.UI.Extensions
//{
//    [AddComponentMenu("UI/Extensions/Primitives/UI Circle Advanced")]
//    public class UICircleAdvanced : MaskableGraphic
//    {
//        [SerializeField]
//        Texture m_Texture;
//        //public float radius = 1;
//        [Range(0, 360)]
//        public int fillPercent = 360;
//        public bool fill = true;
//        public float thickness = 5;
//        [Range(-100, 100)]
//        public float xOffset = 0;
//        [Range(-100, 100)]
//        public float yOffset = 0;
//        [Range(0, 360)]
//        public int segments = 360;

//        public override Texture mainTexture
//        {
//            get
//            {
//                return m_Texture == null ? s_WhiteTexture : m_Texture;
//            }
//        }


//        /// <summary>
//        /// Texture to be used.
//        /// </summary>
//        public Texture texture
//        {
//            get
//            {
//                return m_Texture;
//            }
//            set
//            {
//                if (m_Texture == value)
//                    return;

//                m_Texture = value;
//                SetVerticesDirty();
//                SetMaterialDirty();
//            }
//        }


//        void Update()
//        {
//            this.thickness = (float)Mathf.Clamp(this.thickness, 0, rectTransform.rect.width / 2);
//        }

//        protected UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
//        {
//            UIVertex[] vbo = new UIVertex[4];
//            for (int i = 0; i < vertices.Length; i++)
//            {
//                var vert = UIVertex.simpleVert;
//                vert.color = color;
//                vert.position = vertices[i];
//                vert.uv0 = uvs[i];
//                vbo[i] = vert;
//            }
//            return vbo;
//        }


//        protected override void OnPopulateMesh(VertexHelper vh)
//        {
//            float outer = rectTransform.pivot.x * rectTransform.rect.width;
//            float inner = rectTransform.pivot.x * rectTransform.rect.width - this.thickness;

//            vh.Clear();

//            Vector2 prevX = new Vector2(xOffset, yOffset);
//            Vector2 prevY = new Vector2(xOffset, yOffset);
//            //Vector2 prevX = Vector2.zero;
//            //Vector2 prevY = Vector2.zero;
//            Vector2 uv0 = new Vector2(0, 0);
//            Vector2 uv1 = new Vector2(0, 1);
//            Vector2 uv2 = new Vector2(1, 1);
//            Vector2 uv3 = new Vector2(1, 0);
//            Vector2 pos0;
//            Vector2 pos1;
//            Vector2 pos2;
//            Vector2 pos3;

//            //Determine start position and end position to have the right angle
//            float yOffset2 = Mathf.Pow(yOffset, 2);
//            float xOffset2 = Mathf.Pow(xOffset, 2);
//            float r2 = Mathf.Pow(outer, 2);
//            float newX = Mathf.Sqrt((r2 - yOffset2) / r2) * outer;
//            float newY = Mathf.Sqrt((r2 - xOffset2) / r2) * outer;
//            Vector2 startPos = new Vector2(xOffset, newY);
//            Vector2 endPos = new Vector2(newX, yOffset);

//            bool isEnd = false;

//            float f = (this.fillPercent / 360f);
//            float degrees = 360f / segments;
//            int fa = (int)(segments * f);


//            for (int i = 0; i <= fa; i++)
//            {
//                float rad = Mathf.Deg2Rad * (i * degrees);
//                float c = Mathf.Cos(rad);   // X
//                float s = Mathf.Sin(rad);   // Y

//                uv0 = new Vector2(0, 1);
//                uv1 = new Vector2(1, 1);
//                uv2 = new Vector2(1, 0);
//                uv3 = new Vector2(0, 0);

//                if (outer * c < xOffset)
//                {
//                    continue;
//                }

//                if (outer * s < yOffset)
//                {
//                    continue;
//                }

//                pos0 = prevX;
//                pos1 = new Vector2(outer * c, outer * s);

//                // Draw origin     
//                if (fill)
//                {
//                    pos2 = new Vector2(xOffset, yOffset);
//                    pos3 = new Vector2(xOffset, yOffset);

//                }
//                else
//                {

//                    pos2 = new Vector2(inner * c, inner * s);
//                    pos3 = prevY;
//                }

//                if (isEnd)
//                {
//                    pos0 = prevX;
//                    pos1 = endPos;
//                    pos2 = new Vector2(xOffset, yOffset);
//                    pos3 = new Vector2(xOffset, yOffset);
//                    vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
//                    break;
//                }
//                prevX = pos1;
//                prevY = pos2;

//                vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));

//            }
//        }
//    }
//}