Shader "Blur/Internal/Blur-Filter" 
{	
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader 
	{
		Tags { "RenderType"="Blur" "Queue"="Transparent" }
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Zwrite on
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
				float4 col = tex2D(_MainTex, i.uv.xy);
				return col;
			}			
			ENDCG
		}	
	} 
	
	SubShader 
	{
		Tags { "RenderType"="DiffuseBlur" "Queue"="Transparent" }
		
		CGPROGRAM
		#pragma surface surf Lambert
		
		sampler2D _MainTex;
		
		struct Input {
			float2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * float4(1.0);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 

	SubShader 
	{
		Tags { "RenderType"="Opaque"  }
		Pass
		{
			Zwrite on
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct vOUT
			{
				half4 pos : SV_POSITION;
			};

			vOUT vert(appdata_base v)
			{
				vOUT o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			half4 frag(vOUT i) : COLOR
			{
				return half4(0.0);
			}			
			ENDCG
		}	
	} 
	
	 
}