Shader "Hidden/MotionBlur"
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

			sampler2D _CameraDepthTexture;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;
			float _BlurSize;
			float4x4 _PreViewProjMatrix;
			float4x4 _CurViewProjMatrix_Inverse;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float d = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,i.uv);
				float4 curPos = float4(i.uv * 2 - 1, d * 2 - 1, 1);
				float4 WorldPos = mul(_CurViewProjMatrix_Inverse, curPos);
				WorldPos /= WorldPos.w;

				float4 prePos = mul(_PreViewProjMatrix, WorldPos);
				prePos /= prePos.w;

				float2 velocity = (curPos.xy - prePos.xy) / 2.0;
				float2 uv = i.uv;
				float4 col = tex2D(_MainTex, uv);
				for (int i = 1;i < 5;i++) {
					uv += velocity * _BlurSize * _MainTex_TexelSize.xy;
					col += tex2D(_MainTex, uv);
				}
				col /= 5;

				return fixed4(col.xyz,1);
			}
			ENDCG
		}
	}
}
