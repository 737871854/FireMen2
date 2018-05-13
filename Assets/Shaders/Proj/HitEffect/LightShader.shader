Shader "Q5/Proj/HitEffect/LightShader" 
{
	Properties {
		_TintColor ("Rim Color", Color) = (1,1,1,0.5)
		_InnerColor ("Inner Color", Color) = (1,1,1,0.5)
		_InnerColorPower ("Inner Color Power", Range(0.0,1.0)) = 0.5
		_RimPower ("Rim Power", Range(0.0,5.0)) = 2.5
		_AlphaPower ("Alpha Rim Power", Range(0.0,8.0)) = 4.0
		_Input ("All Power", Range(0.0, 10.0)) = 1.0
	}

	SubShader {
		Tags { "Queue" = "Transparent" }

		CGPROGRAM
		#pragma surface surf Lambert alpha
		struct Input {
			float3 viewDir;
			INTERNAL_DATA
		};

		float4 _TintColor;
		float _RimPower;
		float _AlphaPower;
		float _AlphaMin;
		float _InnerColorPower;
		float _Input;
		float4 _InnerColor;
		
		void surf (Input IN, inout SurfaceOutput o) {
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _TintColor.rgb * pow(rim, _RimPower) * _Input + (_InnerColor.rgb * 2 * _InnerColorPower) * _Input;
			o.Alpha = (pow(rim, _AlphaPower)) * _Input;
		}
		ENDCG
	}
	Fallback "VertexLit"
}
