﻿Shader "Custom/InvisibleObject" {
	
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Geometry+2"}
		
		Pass {
        	Stencil {
                Ref 111
                Comp always
                Pass replace
            }
            
        	ColorMask 0
        	Cull Back
        	ZWrite Off
        	
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            half4 frag(v2f i) : SV_Target {
                return half4(1,0,0,1);
            }
            ENDCG
        }
        
        Pass {
        	Stencil {
                Ref 0
                Comp always
                Pass replace
            }
        
        	ColorMask 0
        	Cull Front
        	ZWrite Off
        	
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            half4 frag(v2f i) : SV_Target {
                return half4(1,0,0,1);
            }
            ENDCG
        }
    } 
}
