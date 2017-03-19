Shader "Custom/SoundLitTexture"
{
	Properties
	{
        _MainTex ("Main Texture", 2D) = "white" {}
        _GradientTex ("Gradient", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float4 vpos : TEXCOORD1; //screen uvs for deferred rendering
			};

            uniform sampler2D _GlobalRenderedSoundTex;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.vpos = ComputeScreenPos (OUT.vertex);

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _GradientTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord);

                c.rgb *= tex2Dproj(_GlobalRenderedSoundTex, UNITY_PROJ_COORD(IN.vpos)).rgb;
				return c;
			}
		ENDCG
		}
	}
}