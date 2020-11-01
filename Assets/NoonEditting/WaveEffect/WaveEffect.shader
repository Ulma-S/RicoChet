Shader "Unlit/WaveEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width("Width", float) = 16
        _Height("Height", float) = 9
        _PassedTime("PassedTime", float) = 0
        _TimeScale("TimeScale", float) = 1
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent"
            "Queue" = "Transparent"
        }
        LOD 100

        GrabPass{
            "_GrabTex"
        }

        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _GrabTex;
            float4 _GrabTex_ST;

            float _PassedTime;
            float _Width, _Height, _TimeScale;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeGrabScreenPos(o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 aspect = float2(_Width/_Height, 1);
                float4 col = 0;
                
                float scale = 0.;
                
                float2 grabUV = (i.screenPos.xy / (i.screenPos.w));

                float2 uv = (i.uv - .5) * aspect;
                
                float x = 0;
                float y = 0;

                float2 centerPos = (uv - float2(x, y));

                float peak = smoothstep(0.4 + scale, 0.3 + scale, length( centerPos )) * smoothstep(0.2 + scale, 0.3 + scale, length(centerPos));
                peak = saturate(peak);
                
                float2 colUV = grabUV + ((centerPos / length(centerPos)) * peak *0.1);

                //col = peak;
                col = tex2D(_GrabTex, colUV);
                //col = tex2D(_GrabTex, grabUV);
                //float4 main = tex2D(_MainTex, i.uv);
                //float alpha = step( 0.2, main.a);
                //col += main * alpha;
                //col = main;

                return col;
            }
            ENDCG
        }
    }
}
