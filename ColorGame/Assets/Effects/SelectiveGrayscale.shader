Shader "Hidden/SelectiveGrayscale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

            #include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            float4 frag (v2f i) : SV_Target
            {
				float4 col = tex2D(_MainTex, i.uv);
				float h = 0;
				float max = 0;
				float min = 1;
				max = col.r > col.g ? (col.r > col.b ? col.r : col.b) : (col.g > col.b ? col.g : col.b);
				min = col.r < col.g ? (col.r < col.b ? col.r : col.b) : (col.g < col.b ? col.g : col.b);
				if (max == col.r) {
					h = 60 * (((col.g - col.b)/(max - min)) % 6);
				}
				else {
					if (max == col.g) {
						h = 60 * ((col.b - col.r) / (max - min) + 2);
					}
					else {
						h = 60 * ((col.r - col.g) / (max - min) + 4);
					}
				}
				if(h < 345 && h > 15)
				{
					col.rgb = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
				}
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
