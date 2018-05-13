// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Q5/Proj/effect/GrowAdd" 
{
    Properties 
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_DissolveTex ("Dissolve Texture", 2D) = "white" {}
		_Percent ("Percent", Range(0.0, 1.0)) = 0.0
		_BrightFactor ("Bright Factor", Range(0.0, 4.0)) = 1.0
		_Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader 
    {
        Tags {"Queue"="Transparent"}
        Pass
        {
			Blend SrcAlpha One
			Cull Off 
			ZWrite Off

            CGPROGRAM
            //#pragma vertex vert_img
			#pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
			#pragma multi_compile_particles
            
			struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			sampler2D _DissolveTex;
			fixed4 _DissolveTex_ST;
			float _Percent;
			float _BrightFactor;
			float4 _Color;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.texcoord1 = TRANSFORM_TEX(v.texcoord, _DissolveTex);
				o.color = v.color;
				return o;
			}

            float4 frag(v2f i) : COLOR
            {
				float maskValue = tex2D(_DissolveTex,i.texcoord1).r;
				if (_Percent >= maskValue){
					discard;
				}
				float4 texColor = tex2D(_MainTex,i.texcoord);
				texColor.rgb = texColor.rgb * _Color.rgb * _BrightFactor;
				texColor.a = texColor.a * i.color.a * _Color.a;
				return texColor;
			}
            
            ENDCG
        }
    } 
    
    FallBack Off
}