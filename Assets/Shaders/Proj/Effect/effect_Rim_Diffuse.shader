// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Q5/Proj/effect/Rim_Diffuse"{
	Properties {
		_DiffuseFactor ("Diffuse Factor", Range(0, 4)) = 1.0
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_RimColor ("Rim Color", Color) = (0.5, 0.5, 0.5, 0.5)
		_RimPower ("Rim Power", Range(0.0, 5.0)) = 2.5
		_AlphaPower ("Alpha Rim Power", Range(0.0, 8.0)) = 4.0
		_AllPower ("All Power", Range(0.0, 10.0)) = 1.0
	}
	SubShader {
		
		Tags {"Queue"="Transparent"}		
		Pass {  
			Blend SrcAlpha OneMinusSrcAlpha
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

			float4 _TintColor;
			float _DiffuseFactor;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col =  tex2D(_MainTex, i.texcoord) * _DiffuseFactor;
				return col;
			}
			ENDCG
		}
		
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			float4 _RimColor;
			float _RimPower;
			float _AlphaPower;
			float _AlphaMin;
			float _AllPower;


			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex)); 
				//float dot = 1 - dot(v.normal, viewDir); 
				half rim = 1.0 - saturate(dot(normalize(viewDir), v.normal));
				float3 rimColor = _RimColor.rgb * pow (rim, _RimPower) * _AllPower;
				o.color.rgb = rimColor;
				//o.Alpha = (pow (rim, _AlphaPower))*_AllPower;
				o.color.a = (pow (rim, _AlphaPower)) * _AllPower;
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				//fixed4 col = fixed4(1, 0, 0, 0.5);
				float4 col = i.color;
				return col;
			}
			ENDCG
		}
		
	}

}

/*
Shader "Q5/Proj/effect/Rim_Diffuse" {
	Properties {
		_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,0.5)
		//_InnerColor ("Inner Color", Color) = (0.5,0.5,0.5,0.5)
		//_InnerColorPower ("Inner Color Power", Range(0.0,1.0)) = 0.5
		_RimPower ("Rim Power", Range(0.0,5.0)) = 2.5
		_AlphaPower ("Alpha Rim Power", Range(0.0,8.0)) = 4.0
		_AllPower ("All Power", Range(0.0, 10.0)) = 1.0
		_MainTex ("Base (RGB)", 2D) = "white" {}

	}
	SubShader {
		Tags { "Queue" = "Transparent" }

		CGPROGRAM
		#pragma surface surf Lambert alpha
		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			INTERNAL_DATA
		};
		float4 _RimColor;
		float _RimPower;
		float _AlphaPower;
		float _AlphaMin;
		//float _InnerColorPower;
		float _AllPower;
		//float4 _InnerColor;
		sampler2D _MainTex;

		void surf (Input IN, inout SurfaceOutput o) {
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			float3 rimColor = _RimColor.rgb * pow (rim, _RimPower) * _AllPower;

			//float3 texColor = tex2D(_MainTex, IN.uv_MainTex) * pow (rim, _RimPower) * _AllPower;
			float4 texColor = tex2D(_MainTex, IN.uv_MainTex);

			o.Emission = rimColor + texColor;
			//o.Emission = rimColor;
			//o.Emission = texColor;


			o.Alpha = (pow (rim, _AlphaPower))*_AllPower * texColor.a;
			//o.Alpha = 0;
		}
		ENDCG
	}
	Fallback "Q5/Unity/Legacy Shaders/VertexLit"
} 
*/
