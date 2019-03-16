Shader "Custom/VertexColors" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows
			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
				float4 color : COLOR;
			};

			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutputStandard o) {
				o.Albedo = _Color.rgb * IN.color;
				o.Alpha = _Color.a;
			}
			ENDCG
		}
		FallBack "Diffuse"
}