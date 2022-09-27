Shader "Custom/ImgEffShader" {

    Properties {
        _MainTex ("Scene Render", 2D) = "white" {}
    }

    SubShader {
        Cull Off ZWrite Off ZTest Always

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f IN) : SV_Target {
                //fixed4 col = tex2D(_MainTex, i.uv); //basic
                fixed4 col = tex2D(_MainTex, IN.uv - float2(0, sin(_Time[2] + IN.vertex.x/80) / 80)); //drunk eff

                //float3 buffer = floor(col.rgb * 7);
                //col.rgb = buffer.rgb / 7;
                //fixed3 purple = { 0.293, 0, 0.508 };
                //purple = 2 * purple;
                //col.rgb = col.rgb * purple.rgb;



                return col;
            }
            ENDCG
        }
    }
}
