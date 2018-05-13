// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Q5/Proj/effect/Foamalpha" {
	Properties{
   		        _Offset ("Offset",Range (0.00,1.00)) = 1.00
		        _Color ("Main Color", Color) = (1,1,1,1)
				_MainTex ("Base (RGB)", 2D) = "white" {}
				_Cutout ("Mask (A)", 2D ) = "white" {}
		}

       SubShader{
	   // µþ°µ
	   
            Tags { "Queue"= "Transparent-110"} // water uses -120
            ZWrite Off
            //Cull Off
            //Blend SrcAlpha One
			Blend SrcAlpha OneMinusSrcAlpha
            //ColorMask RGB
		
		// µþÁÁ
		/*
			Tags { "Queue"= "Transparent-110"} // water uses -120
            ZWrite Off
            Cull Off
            Blend SrcAlpha One
			//Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
		*/	 
			// µþÁÁ
			/*
            Pass {
                Color[_Color]
				SetTexture [_MainTex] {constantcolor[_Color]combine texture * primary double, texture * constant double}
				SetTexture [_Cutout] { combine previous * texture }
	        }
			*/
			// µþ°µ
			
			Pass{
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
					float2 texcoord1 : TEXCOORD1;
				};

				sampler2D _MainTex;
				fixed4 _MainTex_ST;
				sampler2D _Cutout;
				fixed4 _Cutout_ST;
				fixed4 _Color;
				
				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.texcoord1 = TRANSFORM_TEX(v.texcoord, _Cutout);
					return o;
				}
			
				fixed4 frag (v2f i) : SV_Target
				{
					fixed4 mainCol = tex2D(_MainTex, i.texcoord) ;
					fixed4 cutoutCol = tex2D(_Cutout, i.texcoord1);
					return mainCol * _Color  * 2 * cutoutCol * 2;
					//return  cutoutCol * _Color * 2;
					//return  mainCol * _Color * 2;
				}
				ENDCG
			}
		}
}
