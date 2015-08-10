Shader "Blur/Unlit"
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader 
	{
		Tags { "RenderType"="Blur"}
		
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			struct vOUT
			{
				float4 pos : SV_POSITION;
				fixed2 uv;
			};
			
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			
			vOUT vert(appdata_base v)
			{
				vOUT o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			float4 frag(vOUT i) : COLOR
			{
				return tex2D(_MainTex, i.uv);
			}
			
			ENDCG
		}
	}
}
