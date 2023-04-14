Shader "Custom/Vaporwave" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Distortion ("Distortion", Range(0, 1)) = 0.5
        _Speed ("Speed", Range(0, 10)) = 1
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader {
        Tags {"RenderType"="Opaque"}
        LOD 200

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Distortion;
            float _Speed;
            float4 _Color;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float2 uv = i.uv;
                uv.x += sin(uv.y * _Distortion + _Time.y * _Speed) * 0.05;
                uv.y += sin(uv.x * _Distortion + _Time.y * _Speed) * 0.05;
                float4 col = tex2D(_MainTex, uv);
                col *= _Color;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
