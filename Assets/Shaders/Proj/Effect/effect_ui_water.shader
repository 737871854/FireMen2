// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Q5/Proj/effect/UIWater" {
Properties {
	[PerRendererData] _MainTex ("Base (RGB)", 2D) = "white" {}
	_Percent ("Percent", Range(0.00, 1.00)) = 1.0
	_WaveA ("Wave Amplitude", float) = 0.02
	_WaveW ("Wave Angular Speed", float) = 10
	_WaveT ("Wave Time Factor", float) = 8
	_WaterA ("Water Amplitude", float) = 0.01
	_WaterW ("Water Angular Speed", float) = 100
	_WaterT ("Water Time Factor", float) = 10
}

SubShader {
	Tags {"Queue"="Transparent"}
	Blend SrcAlpha OneMinusSrcAlpha  
	LOD 100
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
				float2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Percent;
			float _WaveA;
			float _WaveW;
			float _WaveT;
			float _WaterA;
			float _WaterW;
			float _WaterT;
		
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed uvX = i.texcoord.x;
				fixed uvY = i.texcoord.y;
				//fixed deltaY = 0.02 * sin(10 * uvX + 8 * _Time.y);
				fixed deltaY = _WaveA * sin(_WaveW * uvX + _WaveT * _Time.y);
				fixed limitY = uvY + deltaY;
				if ( limitY >= _Percent)
				{
					discard;
				}
				//fixed deltaX = 0.01 * sin(100 * uvY + _Time.y * 10) ;
				fixed deltaX = _WaterA * sin(_WaterW * uvY + _Time.y * _WaterT) ;
				uvX = uvX - deltaX;
				fixed4 col = tex2D(_MainTex, fixed2(uvX, uvY));
				return col;
			}
		ENDCG
	}
}

}
