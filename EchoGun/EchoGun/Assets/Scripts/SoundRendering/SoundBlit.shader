﻿Shader "Custom/SoundBlit"
{
	Properties
	{
        [NoScaleOffset] _MainTex ("Main Texture", 2D) = "white" {}
        [NoScaleOffset] _RenderedSoundTex("Rendered Sound Texture", 2D) = "white" {}
        [NoScaleOffset] _RenderedBackgroundTex("Rendered Sound Texture", 2D) = "white" {}
        _EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
	}

	SubShader
	{
		Tags
		{ 
			"PreviewType"="Plane"
		}

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
            uniform sampler2D _RenderedSoundTex;
            uniform sampler2D _RenderedBackgroundTex;
            uniform fixed4 _EdgeColor;

            struct appdata_t
			{
				float4 vertex   : POSITION;
                float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 vpos : TEXCOORD1; //screen uvs for deferred rendering
			};

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                OUT.vpos = ComputeScreenPos (OUT.vertex);

				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed3 illuminatedColor = tex2D(_MainTex, IN.uv).rgb;

                float4 projCoord = UNITY_PROJ_COORD(IN.vpos);

                float2 soundValues = tex2Dproj(_RenderedSoundTex, projCoord).rg;
                fixed3 backgroundColor = tex2Dproj(_RenderedBackgroundTex, projCoord).rgb;

                float illumStrength = soundValues.r;

                fixed3 output = lerp(backgroundColor, illuminatedColor, illumStrength);

                float edgeStrength = soundValues.g;

                output = lerp(output, _EdgeColor, edgeStrength);

				return fixed4(output, 1);
			}
		ENDCG
		}
	}
}