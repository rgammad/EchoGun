Shader "Custom/SoundIntensityWrite" {

	Properties
	{
        _MainTex ("Sprite Texture", 2D) = "white" {}
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

		Cull Back
		Lighting Off
		ZWrite Off
        ZTest Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
                float4 objPos : TEXCOORD0;
			};

            sampler2D _MainTex;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.objPos = 2 * IN.vertex;// * 2;// quads are [-0.5..0.5], scale to [-1..1]
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
                float3 sqrDistVec = IN.objPos * IN.objPos;
                float sqrDist = sqrDistVec.x + sqrDistVec.y + sqrDistVec.z;
                if(sqrDist > 1)
                {   
                    return fixed4(0, 0, 0, 0);
                }
				float alpha = sqrDist;
                float intensity = 1 - sqrDist;
				return fixed4(intensity, intensity, intensity, alpha);
               
			}
		ENDCG
		}
	}
}