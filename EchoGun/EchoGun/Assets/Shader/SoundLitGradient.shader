Shader "Custom/SoundLitGradient"
{
	Properties
	{
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
		Blend One One

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
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
                float2 worldPos : TEXCOORD1;
			};

            uniform int _GLOBAL_PING_COUNT;
            uniform float2 _GLOBAL_PING_POS [10];
            uniform float _GLOBAL_TIME_SINCE_PING [10];
            uniform float _GLOBAL_PING_RANGE [10];

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.worldPos = mul(unity_ObjectToWorld, IN.vertex).xy;

				return OUT;
			}

			sampler2D _GradientTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = fixed4(0, 0, 0, 0);

                float totalPingProgress = 0;
                for (int i = 0; i < _GLOBAL_PING_COUNT; i ++)
				{
                    float travelSpeed = 50;
                    float pingDistance = distance(IN.worldPos, _GLOBAL_PING_POS[i]);
                    float travelTime = pingDistance / travelSpeed;

                    //1 if we are within range, 0 otherwise
                    //TODO: consider squared range?
                    float pingProgress = step(pingDistance, _GLOBAL_PING_RANGE[i]);

                    //if(pingProgress > 0) { //possible optimization?

                        //1 when the wave reaches us, 0 when 5 seconds after the wave has reached us.
                        pingProgress *= 1 - ((_GLOBAL_TIME_SINCE_PING[i] - travelTime)/1);
                        totalPingProgress = max(pingProgress, totalPingProgress);
                    //}

                    fixed4 gradientColor = tex2D(_GradientTex, float2(totalPingProgress, 0.5));
                    c.rgb += gradientColor.rgb * gradientColor.a;
                }
				return c;
			}
		ENDCG
		}
	}
}