Shader "Q5/Proj/effect/TVG/FlowLight2"
{
	Properties 
	{
_MainTex ("Base (RGB)", 2D) = "white" {}
		_Split2("──────────────────────────────────────────", Float) = 0.0
		_AdjustMap("-偏色贴图", 2D) = "black" {}
		_AdjustColor_R ("红通道控制颜色", Color) = (0.0, 0.0, 0.0, 1.0)
		_AdjustColor_G ("绿通道控制颜色", Color) = (0.0, 0.0, 0.0, 1.0)
		_AdjustColor_B ("蓝通道控制颜色", Color) = (0.0, 0.0, 0.0, 1.0)
		_AdjustColor_A ("A通道控制颜色", Color) = (0.0, 0.0, 0.0, 1.0)
		_AdjustPower("偏色强度", Range(0.1, 2.0)) = 1.0
		_Split4("──────────────────────────────────────────", Float) = 0.0
		_Flow_MainTex("流光主贴图", 2D) = "white" {}
		_Flow_Color01("流光主贴图配色", Color) = (1,1,1,1)
		_Flow_Blend_Texture("流光混合贴图", 2D) = "white" {}
		_Flow_Color02("流光混合贴图配色", Color) = (1,1,1,1)
		_Flow_Blend_Texture01("流光混合贴图2", 2D) = "black" {}
		_Flow_Speed("主贴图流动速度", Float) = 1
		_Flow_Speed01("混合贴图流动速度", Float) = 1
		_Flow_Speed02("混合贴图2流动速度", Float) = 1
		_Flow_Fresnel_Value("Fresnel_Value", Range(0,3) ) = 0.5
		_Flow_Lighten("Lighten", Float) = 1	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Transparent"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}

		CGPROGRAM
#pragma surface surf  BlinnPhongEditor 
#pragma target 3.0
//

sampler2D _MainTex;

		// 流光相关
		half _Flow_Enable;
		sampler2D _Flow_MainTex;
		float4 _Flow_Color01;
		sampler2D _Flow_Blend_Texture;
		float4 _Flow_Color02;
		sampler2D _Flow_Blend_Texture01;
		float _Flow_Speed;
		float _Flow_Speed01;
		float _Flow_Speed02;
		float _Flow_Fresnel_Value;
		float _Flow_Lighten;

const half IGNORE_GRAY = 0.05;
		sampler2D _AdjustMap;
		fixed4 _AdjustColor_R;
		fixed4 _AdjustColor_G;
		fixed4 _AdjustColor_B;
		fixed4 _AdjustColor_A;
		half _AdjustPower;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			/*inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
				half3 spec = light.a * s.Gloss;
				half4 c;
				c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
				c.a = s.Alpha;
				return c;

			}*/


		inline half3 ComputeAlbedo(float2 uv, half4 c)
		{ 
			half4 adj = tex2D (_AdjustMap, uv);
			half3 o = c.rgb;

			if (adj.r > IGNORE_GRAY)
			{
				o = lerp(c.rgb, _AdjustColor_R.rgb, half3(adj.r, adj.r, adj.r) * _AdjustPower);
			}
			else if (adj.g > IGNORE_GRAY)
			{
				o = lerp(c.rgb, _AdjustColor_G.rgb, half3(adj.g, adj.g, adj.g) * _AdjustPower);
			}
			else if (adj.b > IGNORE_GRAY)
			{
				o = lerp(c.rgb, _AdjustColor_B.rgb, half3(adj.b, adj.b, adj.b) * _AdjustPower);
			}
			else if (adj.a > IGNORE_GRAY)
			{
				o = lerp(c.rgb, _AdjustColor_A.rgb, half3(adj.a, adj.a, adj.a) * _AdjustPower);
			}
			return o;
		}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;   //漫反射基数光
				res.w = spec * Luminance (_LightColor0.rgb); //高光
				res *= atten * 2.0; 

				//return LightingBlinnPhongEditor_PrePass( s, res );
				half3 spec2 = res.a * s.Gloss;
				half4 c;
				//c.rgb = (s.Albedo * res.rgb + res.rgb * spec2);
				c.rgb =  (s.Albedo *res.rgb) + res.rgb * spec2;
				c.a = s.Alpha;
				return c;

			}
			
			struct Input {
				float3 viewDir;
				float2 uv_MainTex;
			float2 uv_Flow_MainTex;
			float2 uv_Flow_Blend_Texture;
			float2 uv_Flow_Blend_Texture01;
			float4 color : COLOR;
				float2 uv_AdjustMap;

			};

			void surf (Input IN, inout EditorSurfaceOutput o) {
			o.Normal = float3(0.0,0.0,1.0);
	
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				

					float4 Fresnel0_1_NoInput = float4(0,0,1,1);
					float4 Fresnel0=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( Fresnel0_1_NoInput.xyz ) )).xxxx;
					float4 Pow0=pow(Fresnel0,_Flow_Fresnel_Value.xxxx);
					float4 Multiply2=_Time * _Flow_Speed.xxxx;
					float4 UV_Pan1=float4((IN.uv_MainTex.xyxy).x,(IN.uv_Flow_MainTex.xyxy).y + Multiply2.x,(IN.uv_Flow_MainTex.xyxy).z,(IN.uv_Flow_MainTex.xyxy).w);
					float4 Tex2D0=tex2D(_Flow_MainTex,UV_Pan1.xy);
					float4 Multiply5=_Flow_Color01 * Tex2D0;

					float4 Multiply1=_Time * _Flow_Speed01.xxxx;
					float4 UV_Pan0=float4((IN.uv_Flow_Blend_Texture.xyxy).x,(IN.uv_Flow_Blend_Texture.xyxy).y + Multiply1.x,(IN.uv_Flow_Blend_Texture.xyxy).z,(IN.uv_Flow_Blend_Texture.xyxy).w);
					float4 Tex2D1=tex2D(_Flow_Blend_Texture,UV_Pan0.xy);

					float4 Multiply6=_Flow_Color02 * Tex2D1;
					float4 Add0=Multiply5 + Multiply6;
					float4 Multiply0=Tex2D0 * Tex2D1;
					float4 Multiply7=Add0 * Multiply0;
					float4 Multiply3=Pow0 * Multiply7;
					float4 Multiply10=_Time * _Flow_Speed02.xxxx;
					float4 UV_Pan2=float4((IN.uv_Flow_Blend_Texture01.xyxy).x,(IN.uv_Flow_Blend_Texture01.xyxy).y + Multiply10.x,(IN.uv_Flow_Blend_Texture01.xyxy).z,(IN.uv_Flow_Blend_Texture01.xyxy).w);
					float4 Tex2D2=tex2D(_Flow_Blend_Texture01,UV_Pan2.xy);
					float4 Multiply8=Multiply3 * Tex2D2;
					float4 Multiply9=Multiply8 * _Flow_Lighten.xxxx;
					float4 Multiply4=Multiply9 * IN.color;
				o.Emission = Multiply4;

				o.Normal = normalize(o.Normal);

				half4 c = tex2D (_MainTex, IN.uv_MainTex);
				o.Albedo = ComputeAlbedo(IN.uv_AdjustMap, c);
				o.Alpha = c.a;
			}
		ENDCG
	}
	Fallback "Diffuse"
}