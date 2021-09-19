// see also...https://github.com/TwoTailsGames/Unity-Built-in-Shaders/blob/master/DefaultResourcesExtra/VR/Shaders/SpatialMappingWireframe.shader
Shader "unity_test/wireframe"
{
    Properties
    {
		_LineColor ("LineColor", Color) = (1, 1, 1, 1)
		_FillColor ("FillColor", Color) = (0, 0, 0, 0)
		_WireThickness ("Wire Thickness", RANGE(0, 500)) = 200
        [MaterialToggle] _IsDiscard ("Use discard", float) = 0
        _CameraPosition ("Camera position", Vector) = (0, 0, 0, 0)
    }

    SubShader
    {
        Cull Off
        ZWrite On
        ZTest On

        Tags { 
            "Queue" = "Transparent-1"
            "RenderType"="Transparent"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(float4, _LineColor)
            UNITY_DEFINE_INSTANCED_PROP(float4, _FillColor)
            UNITY_DEFINE_INSTANCED_PROP(float, _WireThickness)
            UNITY_DEFINE_INSTANCED_PROP(float, _IsDiscard)
            UNITY_DEFINE_INSTANCED_PROP(float4, _CameraPosition)
            UNITY_DEFINE_INSTANCED_PROP(float, _BoundaryLength)
            UNITY_DEFINE_INSTANCED_PROP(float, _EnableInnerSide)
            UNITY_INSTANCING_BUFFER_END(Props)

            struct appdata
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2g
            {
                float4 projectionSpaceVertex : SV_POSITION;
                float4 worldSpacePosition : TEXCOORD1;
                float3 diff : NORMAL;
                UNITY_VERTEX_OUTPUT_STEREO_EYE_INDEX
            };

            struct g2f
            {
                float4 projectionSpaceVertex : SV_POSITION;
                float4 worldSpacePosition : TEXCOORD0;
                float4 dist : TEXCOORD1;
                float3 diff : NORMAL;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2g vert (appdata v)
            {
                v2g o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT_STEREO_EYE_INDEX(o);

                o.projectionSpaceVertex = UnityObjectToClipPos(v.vertex);
                o.worldSpacePosition = mul(unity_ObjectToWorld, v.vertex);

                float4 camera_position = UNITY_ACCESS_INSTANCED_PROP(Props, _CameraPosition);
                o.diff = mul(unity_ObjectToWorld, v.vertex) - camera_position;

                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g i[3], inout TriangleStream<g2f> triangleStream)
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i[0]);

                float2 p0 = i[0].projectionSpaceVertex.xy / i[0].projectionSpaceVertex.w;
                float2 p1 = i[1].projectionSpaceVertex.xy / i[1].projectionSpaceVertex.w;
                float2 p2 = i[2].projectionSpaceVertex.xy / i[2].projectionSpaceVertex.w;

                float2 edge0 = p2 - p1;
                float2 edge1 = p2 - p0;
                float2 edge2 = p1 - p0;

                float area = abs(edge1.x * edge2.y - edge1.y * edge2.x);
                float wireThickness = 800 - UNITY_ACCESS_INSTANCED_PROP(Props, _WireThickness);

                g2f o;
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.worldSpacePosition = i[0].worldSpacePosition;
                o.projectionSpaceVertex = i[0].projectionSpaceVertex;
                o.dist.xyz = float3( (area / length(edge0)), 0.0, 0.0) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                o.diff = i[0].diff;
                triangleStream.Append(o);

                o.worldSpacePosition = i[1].worldSpacePosition;
                o.projectionSpaceVertex = i[1].projectionSpaceVertex;
                o.dist.xyz = float3(0.0, (area / length(edge1)), 0.0) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                o.diff = i[1].diff;
                triangleStream.Append(o);

                o.worldSpacePosition = i[2].worldSpacePosition;
                o.projectionSpaceVertex = i[2].projectionSpaceVertex;
                o.dist.xyz = float3(0.0, 0.0, (area / length(edge2))) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                o.diff = i[2].diff;
                triangleStream.Append(o);
            }

            fixed4 frag (g2f i) : SV_Target
            {
                float l = length(i.diff);

                float minDistanceToEdge = min(i.dist[0], min(i.dist[1], i.dist[2])) * i.dist[3];

                float isUseDicade = UNITY_ACCESS_INSTANCED_PROP(Props, _IsDiscard);

                if(minDistanceToEdge > 0.9)
                {
                    if (isUseDicade == 1.0) {
                        discard;
                    }
                    return UNITY_ACCESS_INSTANCED_PROP(Props, _FillColor);
                }

                return UNITY_ACCESS_INSTANCED_PROP(Props, _LineColor);
            }
            ENDCG
        }
    }
}
