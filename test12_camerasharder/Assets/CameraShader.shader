Shader "Custom/CameraSharder"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_BlendFactor ("Blend Factor", Range (0, 1) ) = 1
	}

	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	sampler2D _MainTex;
	float2 _MainTex_Size;
	float4 _Color;
	float _BlendFactor;

	float4 frag(v2f_img i) : SV_Target
	{
		float4 maintex = tex2D(_MainTex, i.uv);
		
		float4 c = maintex * _BlendFactor + _Color * (1.0 - _BlendFactor);
		
		return c;
	}

	ENDCG

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
	}
	FallBack "Diffuse"
}
