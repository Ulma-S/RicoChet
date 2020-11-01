Shader "Noon/Transition/CircleWipe"
{
	Properties
	{
		_Radius ("Radius", Range(0,3)) = 1.5
		_CenterPosX("CenterPositionX", Range(-1,1)) = 0
		_CenterPosY("CenterPositionY", Range(-1,1)) = 0
		_ScreenHeight("ScreenHeight", Float) = 9
		_ScreenWidth("ScreenWidth", Float) = 16
	}

	SubShader
	{
		
		Pass
		{
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

			float _Radius;
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

			fixed4 frag (v2f i) : COLOR{
			
				float aspectWpH = _ScreenWidth / _ScreenHeight;

				i.uv -= fixed2(0.5,0.5);
				i.uv.x *= aspectWpH;

				float dist = distance(i.uv,fixed2(_CenterPosX , _CenterPosY / aspectWpH));

				if (dist < _Radius) {
					discard;
				}
				return fixed4(0.0,0.0,0.0,1.0);
			}
			ENDCG
		}
	}
}
