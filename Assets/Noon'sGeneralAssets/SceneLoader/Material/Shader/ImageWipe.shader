
Shader "Noon/Transition/ImageWipe"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Scale("Scale", Range(0,10)) = 1
		_Color("Color", Color) = (0,0,0,1)
		_CenterPosX("CenterPositionX", Range(-1,1)) = 0
		_CenterPosY("CenterPositionY", Range(-1,1)) = 0
		_ScreenHeight("ScreenHeight", Float) = 9
		_ScreenWidth("ScreenWidth", Float) = 16
	}

		SubShader
	{

	
		

		Pass
		{

			Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			}

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			fixed4 _Color;
			float _Scale;
			float _CenterPosX;
			float _CenterPosY;
			float _ScreenHeight;
			float _ScreenWidth;

			v2f vert(appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : COLOR{

				float aspectWpH = _ScreenWidth / _ScreenHeight;

				i.uv -= float2(0.5,0.5);
				i.uv.x *= aspectWpH;
				i.uv *= _Scale;

				fixed4 col = tex2D(_MainTex,i.uv + float2(0.5, 0.5));
				
				if (_Scale >= 10) {
					return _Color;
				}
				
				return fixed4(_Color.rgb,col.a);
			}
			ENDCG
		}
	}
}

