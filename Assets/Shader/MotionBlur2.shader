Shader "Hidden/MotionBlur2"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;
			int _InterNum;
			float _Scale;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = i.uv;
				float2 offset = i.uv - float2(0.5, 0.5);
				fixed4 col = tex2D(_MainTex, uv);
				for (int i = 1;i < _InterNum;i++) {
					uv += _Scale * _MainTex_TexelSize.xy * offset;
					col+= tex2D(_MainTex, uv);
				}

				col /= _InterNum;

				return fixed4(col.xyz,1);
			}
			ENDCG
		}
	}
}
