Shader "Q5/Proj/effect/Grow" 
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
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
			#pragma multi_compile_particles
            
			sampler2D _MainTex;
			sampler2D _DissolveTex;
			float _Percent;
			float _BrightFactor;
			float4 _Color;

            float4 frag(v2f_img i) : COLOR
            {
				float maskValue = tex2D(_DissolveTex,i.uv).r;
				if (_Percent >= maskValue){
					discard;
				}
				float4 texColor = tex2D(_MainTex,i.uv);
				texColor.rgb = texColor.rgb * _Color.rgb * _BrightFactor;
				return texColor;
			}
            
            ENDCG
        }
    } 
    
    FallBack Off
}