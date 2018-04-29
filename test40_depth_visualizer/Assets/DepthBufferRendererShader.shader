// see also... https://answers.unity.com/questions/877170/render-scene-depth-to-a-texture.html

Shader "test/DepthBufferRendererShader"
{
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		ZTest Off
		ZWrite Off
		Lighting Off
		AlphaTest Off

		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _CameraDepthTexture;
			uniform half4 _MainTex_TexelSize;

			struct appdata_t {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_t v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.uv);

#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					o.uv.y = 1 - o.uv.y;
#endif
				return o;
			}

			float4 frag(v2f i) : COLOR {
				float d = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv));
			d = 1.0 - Linear01Depth(d);
				return float4(d, d, d, 1.0);
			}

			ENDCG
		}
	}
}