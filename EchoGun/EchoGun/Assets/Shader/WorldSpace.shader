Shader "Custom/WorldSpace"
{
	Properties
	{
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

	SubShader
	{
		Tags
		{ 
            "IgnoreProjector"="True" 
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

			uniform sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;

            struct appdata_t
			{
				float4 vertex   : POSITION;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
                float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_t IN)
			{
				v2f OUT;
                float4 worldPos = mul(unity_ObjectToWorld, float4(IN.vertex.xyz, 1.0));
                OUT.uv = TRANSFORM_TEX(worldPos.xy, _MainTex);
				OUT.vertex = UnityWorldToClipPos(worldPos);

				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				return _Color * tex2D(_MainTex, IN.uv);
			}
		ENDCG
		}
	}
}