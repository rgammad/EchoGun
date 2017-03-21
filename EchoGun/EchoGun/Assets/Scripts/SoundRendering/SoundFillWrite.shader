Shader "Custom/SoundFillWrite" {

	Properties
	{
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Alpha ("Alpha", Range(0, 1)) = 1
        [PerRendererData] _Value ("Written Value", Range(0, 1)) = 1
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
                float2 uv       : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
                float2 uv       : TEXCOORD0;
			};

            sampler2D _MainTex;
            half _Alpha;
            half _Value;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
                fixed textureAlpha = tex2D(_MainTex, IN.uv).a;
                fixed outputValue = textureAlpha * _Value;
                return fixed4(outputValue, 0, 0, _Alpha);
			}
		ENDCG
		}
	}
}