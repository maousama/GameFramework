Shader "Custom/CartoonShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
		#pragma surface surf Toon addshadow fullforwardshadows exclude_path:deferred exclude_path:prepass
        #pragma target 3.0

		fixed4 _Color;
		sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

		inline fixed4 LightingToon(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
		{ 
			half3 normalDir = normalize(s.Normal); 
			float ndl = max(0, dot(normalDir, lightDir));
			fixed3 lightColor = _LightColor0.rgb; 
			fixed4 color; fixed3 diffuse = s.Albedo * lightColor * ndl * atten;
			color.rgb = diffuse; color.a = s.Alpha; return color; 
		}


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
