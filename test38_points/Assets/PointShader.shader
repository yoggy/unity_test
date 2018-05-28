// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "unity_test/PointCloud" {
	Properties {
		_PointSize("Point Size", Float) = 5.0
		_PointColor("Point Color", Color) = (0.5, 0.0, 1.0, 1.0)
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		Pass{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				half3 color : COLOR;
				half psize : PSIZE;
				UNITY_FOG_COORDS(0)
			};

			half _PointSize;
			half3 _PointColor;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.psize = _PointSize;
				o.color = _PointColor;

				UNITY_TRANSFER_FOG(o, o.position);
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				return half4(i.color, 1.0);
			}
			ENDCG
		}
	}
}