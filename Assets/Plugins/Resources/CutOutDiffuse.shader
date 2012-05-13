// Lucifugesさんの提供
Shader "Custom/CharModel"
{
	Properties
	{
		// Debug(竹渕): _Colorの指定がないため色が抜ける現象を回避 
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0, 1)) = 0.6
	}
	
	
	SubShader
	{
		Tags
		{
			"Queue"="AlphaTest"
			"RenderType"="TransparentCutout"
		}
		
		Cull Off
		LOD 200
		
		CGPROGRAM
		#pragma surface surf HalfLambert addshadow dualforward alphatest:_Cutoff
		
		// ここで宣言するとPropertiesが使えるようになるらしい 
		float4 _Color;
		sampler2D _MainTex;
		
		struct Input
		{
			float2 uv_MainTex;
			half4 color : COLOR;
		};
		
		half4 LightingHalfLambert(SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL = dot(s.Normal, lightDir);
			half wrap = NdotL * 0.5 + 0.5;
			half3 lit = _LightColor0.rgb * (wrap * atten);
			return half4(s.Albedo * lit, s.Alpha);
		}
		
		void surf(Input IN, inout SurfaceOutput o)
		{
			
			half4 tex0 = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex0.rgb * IN.color.rgb * _Color.rgb;
			o.Alpha = tex0.a * IN.color.a;
		}
		
		ENDCG
		
	}
	Fallback "Transparent/Cutout/Diffuse"
}