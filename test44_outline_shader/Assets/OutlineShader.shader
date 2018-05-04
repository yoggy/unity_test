// see also... https://answers.unity.com/questions/877170/render-scene-depth-to-a-texture.html

Shader "test/OutlineShader"
{

	Properties
	{
		_MainTex("MainTex", 2D) = "" {}
		_DepthThreshould("depth threshould", Range(0.0, 0.5)) = 0.05
		_SamplingScale("sampling scale", Range(0.5, 10)) = 3  // px
		_OutlineColor("outline color", Color) = (1, 0, 0, 1)
	}

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

			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			float4 _CameraDepthTexture_TexelSize; // see also... https://answers.unity.com/questions/678193/is-it-possible-to-access-the-dimensions-of-a-textu.html
			float _DepthThreshould;
			float _SamplingScale;
			float4 _OutlineColor;

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
				if (_CameraDepthTexture_TexelSize.y < 0)
					o.uv.y = 1 - o.uv.y;
#endif
				return o;
			}

			float sample_depth(float2 uv) {
				return UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, uv));
			}

			float4 frag(v2f i) : COLOR {
				// sobel filter

				float w = _CameraDepthTexture_TexelSize.x * _SamplingScale;

				float d0 = sample_depth(i.uv);
				float d1 = sample_depth(i.uv + float2(w, 0));
				float d2 = sample_depth(i.uv + float2(0, w));
				float d = abs(d0 - d1) + abs(d0 - d2);

				float4 c = tex2D(_MainTex, i.uv);

				if (d > _DepthThreshould) {
					c = + _OutlineColor;
				}

				return c;
			}

			ENDCG
		}		
	}
}