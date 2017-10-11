/// Credit zge
/// Sourced from - http://forum.unity3d.com/threads/draw-circles-or-primitives-on-the-new-ui-canvas.272488/#post-2293224

using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/Primitives/UI Circle Circle")]
    public class UICircleCircle : MaskableGraphic
    {
        [SerializeField]
        Texture m_Texture;
        //NEED TO RETRIEVE THESE VALUES :
        //R1 is the left circle
        public float X1 = 0;
        public float Y1 = 0;
        public float X2 = 0;
        public float Y2 = 0;
        public float R1 = 10;
        public float R2 = 10;
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
                 
            vh.Clear();

            Vector2 ORIGIN1 = new Vector2(X1, Y1);
            Vector2 ORIGIN2 = new Vector2(X2, Y2);
            Vector2 prev;
            Vector2 Pcenter;
            Vector2 PI1;
            Vector2 PI2;
            Vector2 uv0 = new Vector2(0, 0);
            Vector2 uv1 = new Vector2(0, 1);
            Vector2 uv2 = new Vector2(1, 1);
            Vector2 uv3 = new Vector2(1, 0);
            Vector2 pos0;
            Vector2 pos1;
            Vector2 pos2;
            Vector2 pos3;

            // Calculate the distance between centers
            float Dy = Mathf.Abs(ORIGIN1[1] - ORIGIN2[1]);
            float Dx = Mathf.Abs(ORIGIN1[0] - ORIGIN2[0]);
            float Dist = Mathf.Sqrt(Mathf.Pow(Dx, 2) + Mathf.Pow(Dy, 2));
            // Check for solution
            if (Dist > R1 + R2)
            {
                // TODO
            }

            // Find A and H
            float A = (Mathf.Pow(R1, 2) - Mathf.Pow(R2, 2) + Mathf.Pow(Dist, 2)) / (2 * Dist);
            float H = Mathf.Sqrt(Mathf.Pow(R1, 2) - Mathf.Pow(A, 2));

            // Finding the center point in distance vector
            float Pcx = ORIGIN1[0] + A * (ORIGIN2[0]- ORIGIN1[0]) / Dist;
            float Pcy = ORIGIN1[1] + A * (ORIGIN2[1] - ORIGIN1[1]) / Dist;
            Pcenter = new Vector2(Pcx, Pcy);

            // Getting intersection points PI(x3,y3) and PI(x4,y4)
            float X3 = Pcx + H * (ORIGIN2[1] - ORIGIN1[1]) / Dist;
            float Y3 = Pcy - H * (ORIGIN2[0] - ORIGIN1[0]) / Dist;
            float X4 = Pcx - H * (ORIGIN2[1] - ORIGIN1[1]) / Dist;
            float Y4 = Pcy + H * (ORIGIN2[0] - ORIGIN1[0]) / Dist;
            PI1 = new Vector2(X3, Y3); // Start and end
            PI2 = new Vector2(X4, Y4); // Start second part and end first part

            //Debug.Log("PI1" + PI1 + "PI2" + PI2);
            //Debug.Log("PC" + Pcenter);

            prev = PI1;

            float degrees = 360f / segments;
            float radius = R1;
            float shiftX = ORIGIN1[0];
            float shiftY = ORIGIN1[1];
            bool circleOriginPassed = false;
            bool finish = false;
            // check start/end degree
            float xc = (PI1[0] - ORIGIN1[0]) / R1;
            float xc2 = (PI2[0] - ORIGIN1[0]) / R1;
            //float xc2 = (PI2[0] - Pcenter[0]) / R1;
            Debug.Log("RAD:" + Mathf.Acos(xc) + " XC" + xc);
            int startDeg = (int)((Mathf.Acos(xc) * 180 ) / Mathf.PI);
            int endDeg = (int)((Mathf.Acos(xc2) * 180) / Mathf.PI);

            Debug.Log("Start, degree:" + startDeg);
            Debug.Log("END, degree:" + endDeg);
            // Start drawing with radius 1
            for (int i = endDeg; i <= 360; i++) { 
            
                //finish with second radius
                if (i == startDeg)
                {
                    //Debug.Log("endDeg");
                    if (finish == false)
                    {
                        radius = R2;
                        shiftX = ORIGIN2[0];
                        shiftY = ORIGIN2[1];
                        xc = (PI1[0] - ORIGIN2[0]) / R2;
                        xc2 = (PI2[0] - ORIGIN2[0]) / R2;
                        startDeg = (int)((Mathf.Acos(xc) * 180) / Mathf.PI);
                        endDeg = (int)((Mathf.Acos(xc2) * 180) / Mathf.PI);
                        i = startDeg;
                        //endDeg = startDeg;
                        finish = true;
                    }
                    else if (finish == true)
                    {
                        break;
                    }
                }
                //Debug.Log("Start, degree:" + i);
                float rad = Mathf.Deg2Rad * (i * degrees);
                float c = Mathf.Cos(rad);   // X
                float s = Mathf.Sin(rad);   // Y

                uv0 = new Vector2(0, 1);
                uv1 = new Vector2(1, 1);
                uv2 = new Vector2(1, 0);
                uv3 = new Vector2(0, 0);

                //if (radius * c + shiftX < PI2[0])  // THIS IS SWITCHED TO PI1 WITH OTHER CIRCLE
                //{
                //    prev = PI2;
                //    Switch = true;
                //    continue;
                //}

                //if (Switch == true) //AND
                //{
                //    // CHECK FOR NEXT X AND SWITCH AGAIN.
                //}

                //if (radius * c + shiftX < PI1[0] && radius * s + shiftY < PI1[1])
                //{
                //    prev = PI1;
                //    continue;
                //}

                //if (radius == R1 && radius * c + shiftX < PI2[0])
                //{
                //    pos0 = prev;
                //    pos1 = PI2;
                //    pos2 = Pcenter;
                //    pos3 = Pcenter;
                //    vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
                //    //prev = PI2;
                //    radius = R2;
                //    shiftX = ORIGIN2[0];
                //    shiftY = ORIGIN2[1];
                //    part2Start = true;
                //}

                //if (radius == R2 && radius * c + shiftX > PI1[0])
                //{
                //    pos0 = prev;
                //    pos1 = PI1;
                //    pos2 = Pcenter;
                //    pos3 = Pcenter;
                //    vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
                //    //prev = PI1;
                //    radius = R1;
                //    shiftX = ORIGIN1[0];
                //    shiftY = ORIGIN1[1];
                //}

                //if (part2Start == true && radius * c + shiftX > PI2[0])
                //{
                //    continue;
                //}

                //if (part2Start == true && radius * c + shiftX < PI2[0])
                //{
                //    prev = PI2;
                //    part2Start = false;
                //}
                //if (radius == R1 && radius * s + shiftY < PI1[1])
                //{
                //    //Debug.Log("Skipped:" + (radius * s + shiftY));
                //    continue;
                //}

                // Draw start and next
                pos0 = prev;
                pos1 = new Vector2(radius * c + shiftX, radius * s + shiftY);
                // Draw origin       
                pos2 = Pcenter;
                pos3 = Pcenter;
                // Adjust next position
                prev = pos1;
                //Debug.Log(i);
                vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
                if (i == 360 && circleOriginPassed == false)
                {
                    circleOriginPassed = true;
                    i = 0;
                }
            }

        }
    }
}

//BACKUP

    /// Credit zge
/// Sourced from - http://forum.unity3d.com/threads/draw-circles-or-primitives-on-the-new-ui-canvas.272488/#post-2293224

//using System.Collections.Generic;

//namespace UnityEngine.UI.Extensions
//{
//    [AddComponentMenu("UI/Extensions/Primitives/UI Circle Circle")]
//    public class UICircleCircle : MaskableGraphic
//    {
//        [SerializeField]
//        Texture m_Texture;
//        public float X1 = 0;
//        public float Y1 = 0;
//        public float X2 = 0;
//        public float Y2 = 0;
//        public float R1 = 10;
//        public float R2 = 10;
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

//            vh.Clear();

//            Vector2 ORIGIN1 = new Vector2(X1, Y1);
//            Vector2 ORIGIN2 = new Vector2(X2, Y2);
//            Vector2 prev;
//            Vector2 Pcenter;
//            Vector2 PI1;
//            Vector2 PI2;
//            Vector2 uv0 = new Vector2(0, 0);
//            Vector2 uv1 = new Vector2(0, 1);
//            Vector2 uv2 = new Vector2(1, 1);
//            Vector2 uv3 = new Vector2(1, 0);
//            Vector2 pos0;
//            Vector2 pos1;
//            Vector2 pos2;
//            Vector2 pos3;

//            // Calculate the distance between centers
//            float Dy = Mathf.Abs(ORIGIN1[1] - ORIGIN2[1]);
//            float Dx = Mathf.Abs(ORIGIN1[0] - ORIGIN2[0]);
//            float Dist = Mathf.Sqrt(Mathf.Pow(Dx, 2) + Mathf.Pow(Dy, 2));
//            // Check for solution
//            if (Dist > R1 + R2)
//            {
//                // TODO
//            }

//            // Find A and H
//            float A = (Mathf.Pow(R1, 2) - Mathf.Pow(R2, 2) + Mathf.Pow(Dist, 2)) / (2 * Dist);
//            float H = Mathf.Sqrt(Mathf.Pow(R1, 2) - Mathf.Pow(A, 2));

//            // Finding the center point in distance vector
//            float Pcx = ORIGIN1[0] + A * (ORIGIN2[0] - ORIGIN1[0]) / Dist;
//            float Pcy = ORIGIN1[1] + A * (ORIGIN2[1] - ORIGIN1[1]) / Dist;
//            Pcenter = new Vector2(Pcx, Pcy);

//            // Getting intersection points PI(x3,y3) and PI(x4,y4)
//            float X3 = Pcx + H * (ORIGIN2[1] - ORIGIN1[1]) / Dist;
//            float Y3 = Pcy - H * (ORIGIN2[0] - ORIGIN1[0]) / Dist;
//            float X4 = Pcx - H * (ORIGIN2[1] - ORIGIN1[1]) / Dist;
//            float Y4 = Pcy + H * (ORIGIN2[0] - ORIGIN1[0]) / Dist;
//            PI1 = new Vector2(X3, Y3);
//            PI2 = new Vector2(X4, Y4);

//            //Debug.Log("PI1" + PI1 + "PI2" + PI2);
//            //Debug.Log("PC" + Pcenter);

//            prev = PI1;

//            float degrees = 360f / segments;
//            float radius = R1;
//            float shiftX = ORIGIN1[0];
//            float shiftY = ORIGIN1[1];
//            // Start Drawing, origin at Pcenter
//            // Start with radius1, continue to PI
//            // Then Switch to radius 2, continue to first PI.   
//            for (int i = 0; i <= segments; i++)
//            {
//                //Debug.Log("Start, degree:" + i);
//                float rad = Mathf.Deg2Rad * (i * degrees);
//                float c = Mathf.Cos(rad);   // X
//                float s = Mathf.Sin(rad);   // Y

//                uv0 = new Vector2(0, 1);
//                uv1 = new Vector2(1, 1);
//                uv2 = new Vector2(1, 0);
//                uv3 = new Vector2(0, 0);

//                if (radius * c + shiftX < PI2[0])
//                {
//                    pos0 = prev;
//                    pos1 = PI2;
//                    pos2 = Pcenter;
//                    pos3 = Pcenter;
//                    vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
//                    //prev = PI2;
//                    radius = R2;
//                    shiftX = ORIGIN2[0];
//                    shiftY = ORIGIN2[1];
//                }

//                if (radius * c + shiftX > PI1[0])
//                {
//                    pos0 = prev;
//                    pos1 = PI1;
//                    pos2 = Pcenter;
//                    pos3 = Pcenter;
//                    vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
//                    //prev = PI1;
//                    radius = R1;
//                    shiftX = ORIGIN1[0];
//                    shiftY = ORIGIN1[1];
//                }

//                if (radius * s + shiftY > PI2[1])
//                {
//                    //Debug.Log("Skipped:" + (radius * s + shiftY));
//                    continue;
//                }

//                if (radius * s + shiftY < PI1[1])
//                {
//                    //Debug.Log("Skipped:" +(radius * s + shiftY));
//                    continue;
//                }

//                // Draw start and next
//                pos0 = prev;
//                pos1 = new Vector2(radius * c + shiftX, radius * s + shiftY);

//                // Draw origin       
//                pos2 = Pcenter;
//                pos3 = Pcenter;
//                // Adjust next position
//                prev = pos1;

//                //Debug.Log("POS" + pos0 + pos1 + pos2 + pos3);
//                vh.AddUIVertexQuad(SetVbo(new[] { pos0, pos1, pos2, pos3 }, new[] { uv0, uv1, uv2, uv3 }));
//            }
//        }
//    }
//}