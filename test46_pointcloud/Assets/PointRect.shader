Shader "unity_test/PointRect" {
	Properties {
		_PointSize("Point Size", Float) = 5.0
		_PointColor("Point Color", Color) = (0.5, 0.0, 1.0, 1.0)
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		Cull Off
		Pass{
			CGPROGRAM

			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 position : POSITION;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				half3 color : COLOR;
				half psize : PSIZE;
				UNITY_FOG_COORDS(0)
			};

			half _PointSize;
			half3 _PointColor;

			v2f vert(appdata v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.position);
				o.psize = _PointSize;
				o.color = _PointColor;

				UNITY_TRANSFER_FOG(o, o.position);
				return o;
			}

			[maxvertexcount(10)]
			void geom(point v2f i[1], inout TriangleStream<v2f> outputStream)
			{
				float4 org = i[0].position;
				float2 ext = 1.0 / _ScreenParams * _PointSize; // _ScreenParams.xy:rendering target resolution

				v2f o = i[0]; // copy all members

				//
				//  0---1
				//  |   |
				//  2---3

				// 0
				o.position.x = org.x - ext.x;
				o.position.y = org.y + ext.y;
				o.position.zw = org.zw;
				outputStream.Append(o);

				// 1
				o.position.x = org.x + ext.x;
				o.position.y = org.y + ext.y;
				o.position.zw = org.zw;
				outputStream.Append(o);

				// 2
				o.position.x = org.x - ext.x;
				o.position.y = org.y - ext.y;
				o.position.zw = org.zw;
				outputStream.Append(o);

				// 3
				o.position.x = org.x + ext.x;
				o.position.y = org.y - ext.y;
				o.position.zw = org.zw;
				outputStream.Append(o);

				outputStream.RestartStrip();
			}

			half4 frag(v2f i) : SV_Target
			{
				return half4(i.color, 1.0);
			}
			ENDCG
		}
	}
}