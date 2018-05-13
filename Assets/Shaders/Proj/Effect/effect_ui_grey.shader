// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Q5/Proj/effect/UI Grey" {
Properties {
	[PerRendererData] _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_GreyX ("Grey Affect X", Range(0, 1)) = 0.299
	_GreyY ("Grey Affect Y", Range(0, 1)) = 0.587
	_GreyZ ("Grey Affect Z", Range(0, 1)) = 0.114

	_StencilComp ("Stencil Comparison", Float) = 8
	_Stencil ("Stencil ID", Float) = 0
	_StencilOp ("Stencil Operation", Float) = 0
	_StencilWriteMask ("Stencil Write Mask", Float) = 255
	_StencilReadMask ("Stencil Read Mask", Float) = 255

	_ColorMask ("Color Mask", Float) = 15
}

SubShader {
	Tags {"Queue"="Transparent" "RenderType"="Transparent"}
	
	Stencil
	{
		Ref [_Stencil]
		Comp [_StencilComp]
		Pass [_StencilOp] 
		ReadMask [_StencilReadMask]
		WriteMask [_StencilWriteMask]
	}

	LOD 100
	//ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 
	
	Pass {  
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _GreyX, _GreyY, _GreyZ;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				//fixed grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
				fixed grey = dot(col.rgb, fixed3(_GreyX, _GreyY, _GreyZ));
		        col.rgb = fixed3(grey, grey, grey);
				return col;
			}
			ENDCG
		}
	}
}
