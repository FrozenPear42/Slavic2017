// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GrabPass Disortion" 
{
	Properties
	{
		_MainTex("Texture (R,G=X,Y Distortion; B=Mask; A=Unused)", 2D) = "white" {}
		_IntensityAndScrolling("Intensity (XY); Scrolling (ZW)", Vector) = (0.1,0.1,1,1)
		[Toggle(MASK)] _MASK("Texture Blue channel is Mask", Float) = 0
		[Toggle(DEBUGUV)] _DEBUGUV("Debug Texture Coordinates", Float) = 0

		_Fade("Fade", Range(0.0, 1.0)) = 1.0
			
	}


		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" }
		Lighting Off
		Fog{ Mode Off }
		ZWrite Off
		LOD 200

		BindChannels
		{
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}

		GrabPass{ "_GrabTexture" }

		Pass{
		CGPROGRAM
		//#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#pragma shader_feature MASK
#pragma shader_feature DEBUGUV
#include "UnityCG.cginc"

		// _MainTex is our distortion texture and
		// should use "Bypass sRGB Sampling" import setting.
		sampler2D _MainTex;
	float4 _MainTex_ST; // texture tiling and offset

						// _GrabTexture contains the contents of the screen
						// where the object is about to be drawn.
	sampler2D _GrabTexture;

	// x=horizontal intensity, y=vertical intensity
	// z=horizontal scrolling speed, w=vertical scrolling speed
	float4 _IntensityAndScrolling;
	float _Fade;

	struct appdata_t {
		float4 vertex  : POSITION;
		fixed4 color : COLOR;
		half2 texcoord : TEXCOORD0;
	};

	struct v2f 
	{
		float4 vertex  : SV_POSITION;
		fixed4 color : COLOR;
		half2 texcoord : TEXCOORD0;
		half2 screenuv : TEXCOORD1;
		#if MASK
				half2 maskuv   : TEXCOORD2;
		#endif
	};

	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);

		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex); // Apply texture tiling and offset.
		o.texcoord += _Time.gg * _IntensityAndScrolling.zw; // Apply texture scrolling.
		o.color = v.color;
		#if MASK
																	// We don't scroll this texture lookup, because we want the mask to be static.
				o.maskuv = v.texcoord;
		#endif

		half4 screenpos = ComputeGrabScreenPos(o.vertex);
		o.screenuv = screenpos.xy / screenpos.w;
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		half2 distort = tex2D(_MainTex, i.texcoord).xy;

		// distort*2-1 transforms range from 0..1 to -1..1.
		// negative values move to the left, positive to the right.
		half2 offset = (distort.xy * 2 - 1) * _IntensityAndScrolling.xy *_Fade * i.color.a;

		#if MASK
				// _MainTex stores in the blue channel the mask.
				// The mask intensity represents how strong the distortion should be.
				// black=no distortion, white=full distortion
				half  mask = tex2D(_MainTex, i.maskuv).b;
				offset *= mask;
		#endif                        

		// get screen space position of current pixel
		half2 uv = i.screenuv + offset;
		half4 color = tex2D(_GrabTexture, uv);
		UNITY_OPAQUE_ALPHA(color.a);


		#if DEBUGUV
				color.rg = uv;
				color.b = 0;
		#endif


		return color;
	}
		ENDCG
	}
	}
}