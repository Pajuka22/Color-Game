Shader "Hidden/SelectiveGrayscale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Red("RedVisible?", Range(0, 1)) = 0
		_Orange("OrangeVisible?", Range(0, 1)) = 0
		_Yellow("YellowVisible?", Range(0, 1)) = 0
		_Green("GreenVisible?", Range(0, 1)) = 0
		_Blue("BlueVisible?", Range(0, 1)) = 0
		_Purple("PurpleVisible?", Range(0, 1)) = 0
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
			float _Red;
			float _Orange;
			float _Yellow;
			float _Green;
			float _Blue;
			float _Purple;

            float4 frag (v2f i) : SV_Target
            {
				bool red = _Red != 0;
				bool orange = _Orange != 0;
				bool yellow = _Yellow != 0;
				bool green = _Green != 0;
				bool blue = _Blue != 0;
				bool purple = _Purple != 0;

				float4 col = tex2D(_MainTex, i.uv);
				float gray = 0.3*col.r + 0.59*col.g + 0.11*col.b;
				float h = 0;
				float max = 0;
				float min = 255;
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
				if (h < 0) {
					h += 360;
				}
				if (h >= 300 || h <= 17) {
					if (_Red != 0) {
						if (h >= 300 && h <= 330 && _Purple == 0) {
							col.rgb = gray + (col.rgb - gray) * ((h - 300) / 30);
							return col;
						}
						if (h <= 300 && h >= 7 && _Orange == 0) {
							col.rgb = gray + (col.rgb - gray) * ((17 - h) / 10);
						}
					}
					else {
						col.rgb = gray;
					}
				}
				if (h > 17 && h <= 40) {
					if (_Orange != 0) {
						if (_Red == 0) {

						}
						if (_Yellow == 0) {

						}
					}
					else {
						col.rgb = gray;
					}
				}
				if (h > 40 && h <= 70) {
					if (_Yellow != 0) {

					}
					else {
						col.rgb = gray;
					}
				}
				if (h > 70 && h < 180) {
					if (_Green != 0) {

					}
					else {
						col.rgb = gray;
					}
				}
				if (h >= 180 && h <= 270) {
					if (_Blue != 0) {

					}
					else {
						col.rgb = gray;
					}
				}
				if (h > 270 && h < 300) {
					if (_Purple != 0) {

					}
					else {
						col.rgb = gray;
					}
				}
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}