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
				float4 col = tex2D(_MainTex, i.uv);
				float gray = 0.3*col.r + 0.59*col.g + 0.11*col.b;
				float h = 0;
				float max = 0;
				float min = 255;
				max = col.r >= col.g ? (col.r >= col.b ? col.r : col.b) : (col.g >= col.b ? col.g : col.b);
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
				//start red stuff
				if (h >= 300 || h <= 17) {
					if (h >= 300 && h <= 330) {
						col.rgb = gray + (col.rgb - gray) * ((h - 300) / 30 * _Red + (_Purple + _Red) / 2 * (330 - h) / 30);
						return col;
						/*
						the color's rgb values should equal the gray's values + some number * col.rgb - gray
						so that value remains constant, varying saturation.
						that value would normally be _Red, but we want to interpolate between red and purple.
						To do this, we add (h-300)/30 * _Red | 300 <= h <= 300, so that (h-300)/30 is between 1 and 0.
						then we add (330 - h)/30 * average of purple and red saturation so that we can have the average at h = 300.
						(h - 300)/30 == 1 - (330 - h)/30 when 300 <= h <= 300, so the total adds up to one, weighting the saturation
						based on how far between red and purple it is.

						We do the same thing for the rest of them, changing the value so that the 30 is replaced by the difference
						between the max and min of the tweening zone for each color, and replace 300 with the min and 330 with the max.
						*/

						//TL;DR: finds the weighted average of _Red and (_Red + _Purple)/2 and multiplies that by col.rgb - gray, then adds that to gray.
					}
					else if (h <= 17 && h >= 7) {
						col.rgb = gray + (col.rgb - gray) * ((17 - h) / 10 * _Red + (_Orange + _Red) / 2 * (h - 7) / 10);
						return col;
					}
					else {
						col.rgb = gray + (col.rgb - gray) * _Red;
						return col;
					}
				}
				//start orange stuff
				if (h > 17 && h <= 50) {
					if (h >= 40) {
						//col.rgb = gray + (col.rgb - gray) * ((h - 40) / 10 * _Orange + (50 - h) / 10 * (_Orange + _Yellow)/2);
						col.rgb = gray + (col.rgb - gray) * ((h - 40) / 10 * (_Orange + _Yellow) / 2 + (50 - h) / 10 * _Orange);
						return col;
					}
					else if (h <= 30) {
						col.rgb = gray + (col.rgb - gray) * ((30 - h) / 13 * (_Red + _Orange) / 2 + _Orange * (h - 17) / 13);
						return col;
					}
					else {
						col.rgb = gray + (col.rgb - gray) * _Orange;
						return col;
					}
				}
				//start yellow stuff
				if (h >= 50 && h <= 80) {
					if (h > 70) {
						col.rgb = gray + (col.rgb - gray) * ((h - 70) / 10 * (_Yellow + _Green) / 2 + (80 - h) / 10 * _Yellow);
						return col;
					}
					else if (h < 60) {
						col.rgb = gray + (col.rgb - gray) * ((60 - h) / 10 * (_Orange + _Yellow) / 2 + (h - 50) / 10 *_Yellow);
						return col;
					}
					else {
						col.rgb = gray + (col.rgb - gray) * _Yellow;
						return col;
					}
				}
				//start green stuff
				if (h > 80 && h < 180) {
					if (h <= 90) {
						col.rgb = gray + (col.rgb - gray) * ((90 - h) / 10 * (_Green + _Yellow) / 2 + (h - 80) / 10 * _Green);
						return col;
					}
					else if (h >= 150) {
						col.rgb = gray + (col.rgb - gray) * ((h - 150) / 30 * (_Blue + _Green) / 2 + (180 - h) / 30 * _Green);
						return col;
					}
					else {
						col.rgb = gray + (col.rgb - gray) * _Green;
						return col;
					}
				}
				//start blue stuff
				if (h >= 180 && h <= 270) {
					if (h >= 260) {
						col.rgb = gray + (col.rgb - gray) * ((270 - h) / 10 * _Blue + (h - 260) / 10 * (_Purple + _Blue) / 2);
						return col;
					}
					else if (h <= 200) {
						col.rgb = gray + (col.rgb - gray) * ((h - 180) / 20 * _Blue + (200 - h) / 20 * (_Green + _Blue) / 2);
						return col;
					}
					else {
						col.rgb = gray + (col.rgb - gray) * _Blue;
						return col;
					}
				}
				//start purple stuff
				if (h > 270 && h < 300) {
					if (h >= 290) {
						col.rgb = gray + (col.rgb - gray) * ((300 - h) / 10 * _Purple + (h - 290) / 10 * (_Red + _Purple) / 2);
						return col;
					}
					else if (h <= 280) {
						col.rgb = gray + (col.rgb - gray) * ((h - 270) / 10 * _Purple + (280 - h) / 10 * (_Blue + _Purple) / 2);
						return col;
					}
					else {
						col.rgb = gray + (col.rgb - gray) * _Purple;
						return col;
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