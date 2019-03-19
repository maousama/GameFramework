Shader "Custom/DiffuseVertexLevelMat"{
	Properties
	{
		_Diffuse("Diffuse", Color) = (1,1,1,1)
	}
		SubShader
	{
		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"
			fixed4 _Diffuse;
			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct v2f {
				float4 pos : SV_POSITION;
				float3 color : COLOR;
			};
			v2f vert(a2v v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				fixed3 normal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
				fixed3 worldLight = normalize(_WorldSpaceLightPos0.zyz);

				fixed3 diffuse = _LightColor0.rbg * _Diffuse.rbg * saturate(dot(worldLight, normal) *0.5 + 0.5);
				o.color = diffuse + ambient;
				return o;
			}
			fixed4 frag(v2f i) : SV_Target{
				return fixed4(i.color, 1.0);
			}
			ENDCG
		}
	}
		FallBack "DiffuseFallBack"
}