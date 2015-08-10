Shader "Blur/Internal/Blur-PostProcess" 
{
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BlurAmt ("Blur Power", float) = 1.0
		_ScreenWidth ("Screen Width", float) = 512.0
		_ScreenHeight ("Screen Height", float) = 512.0
	}
	SubShader 
	{
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct vOUT
			{
				float4 pos : SV_POSITION;
				fixed2 uv : TEXCOORD0;
			};
			
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _BlurAmt;
			uniform float _ScreenWidth;
			uniform float _ScreenHeight;

			vOUT vert(appdata_base v)
			{
				vOUT o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			half4 frag(vOUT i) : COLOR
			{
				half4 sum = half4(0.0);
 				half blurSizeX = _BlurAmt/_ScreenWidth;

			   sum += tex2D(_MainTex, float2(i.uv.x - 4.0*blurSizeX, i.uv.y)) * 0.05;
			   sum += tex2D(_MainTex, float2(i.uv.x - 3.0*blurSizeX, i.uv.y)) * 0.09;
			   sum += tex2D(_MainTex, float2(i.uv.x - 2.0*blurSizeX, i.uv.y)) * 0.12;
			   sum += tex2D(_MainTex, float2(i.uv.x - blurSizeX, i.uv.y)) * 0.15;
			   sum += tex2D(_MainTex, float2(i.uv.x + blurSizeX, i.uv.y)) * 0.15;
			   sum += tex2D(_MainTex, float2(i.uv.x + 2.0*blurSizeX, i.uv.y)) * 0.12;
			   sum += tex2D(_MainTex, float2(i.uv.x + 3.0*blurSizeX, i.uv.y)) * 0.09;
			   sum += tex2D(_MainTex, float2(i.uv.x + 4.0*blurSizeX, i.uv.y)) * 0.05;
			   return sum;
			}		
			ENDCG
		}
		
		
//		GrabPass { }
		
		Pass
		{
			Blend OneMinusDstColor One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			struct vOUT
			{
				float4 pos : SV_POSITION;
				float2 uv;
			};
			
			 sampler2D _GrabTexture : register(s0);

			uniform float4 _MainTex_ST;
			uniform float _BlurAmt;
			uniform float _ScreenWidth;
			uniform float _ScreenHeight;
			
			vOUT vert(appdata_base v)
			{
				vOUT o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			half4 frag(vOUT i) : COLOR
			{
				half4 sum = half4(0.0);
 				half blurSizeY = _BlurAmt/_ScreenHeight;

			   sum += tex2D(_GrabTexture, float2(i.uv.x, i.uv.y - 4.0*blurSizeY)) * 0.05;
			   sum += tex2D(_GrabTexture, float2(i.uv.x, i.uv.y - 3.0*blurSizeY)) * 0.09;
			   sum += tex2D(_GrabTexture, float2(i.uv.x, i.uv.y - 2.0*blurSizeY)) * 0.12;
			   sum += tex2D(_GrabTexture, float2(i.uv.x, i.uv.y - blurSizeY)) * 0.15;
			   sum += tex2D(_GrabTexture, float2(i.uv.x, i.uv.y + blurSizeY)) * 0.15;
			   sum += tex2D(_GrabTexture, float2(i.uv.x, i.uv.y + 2.0*blurSizeY)) * 0.12;
			   sum += tex2D(_GrabTexture, float2(i.uv.x, i.uv.y + 3.0*blurSizeY)) * 0.09;
			   sum += tex2D(_GrabTexture, float2(i.uv.x, i.uv.y + 4.0*blurSizeY)) * 0.05;
			   return sum;
			}			
			ENDCG
		}
	} 
	//FallBack "Diffuse"
}
