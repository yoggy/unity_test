// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "BackgroundRenderer/BackgroundRenderer" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
        _BackgroundTex ("Background Texture", 2D) = "white" {}
	}
	SubShader {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }

		Pass {
			CGPROGRAM
			sampler2D _MainTex;
			sampler2D _BackgroundTex;

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_img v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET {
				fixed4 c0 = tex2D(_MainTex, i.uv);
				fixed4 c1 = tex2D(_BackgroundTex, i.uv);
				fixed3 rv = lerp(c1.rgb, c0.rgb, c0.a);
				return fixed4(rv.r, rv.g, rv.b, c0.a);
			}
			ENDCG
		}
	}
}
