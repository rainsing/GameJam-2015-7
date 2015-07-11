Shader "Custom/ClippingPlane" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	
	SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        
        Stencil {
       		Ref 111
        	Comp NotEqual
        }
        
        Blend One One
        
        CGPROGRAM
		#pragma surface surf Lambert
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			c.rgb *=  _Color.rgb * 2.0;
			o.Albedo = c.rgb * c.a;
		}
		ENDCG
    } 
}
