Shader "Custom/SoundFillWrite"
{
	Properties
	{
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Alpha ("Alpha", Range(0, 1)) = 1
        [PerRendererData] _Value ("Written Value", Range(0, 1)) = 1
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
            "DisableBatching" = "true"
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;

            fixed _Alpha;
            fixed _Value;

			fixed SampleAlpha (float2 uv)
			{
				fixed alpha;

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				alpha = tex2D (_AlphaTex, uv).r;
#else
                alpha = tex2D (_MainTex, uv).a;
#endif //ETC1_EXTERNAL_ALPHA

				return alpha;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
                fixed textureAlpha = SampleAlpha(IN.texcoord);
                fixed outputValue = textureAlpha * _Value;
                return fixed4(outputValue, 0, 0, _Alpha);
			}
		ENDCG
		}
	}
}
