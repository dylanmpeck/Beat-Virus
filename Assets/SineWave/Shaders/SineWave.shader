// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SineWave" {
	Properties {
		_MainTex ("", 2D) = "white" {}

		[MaterialToggle] _XAxis("isToggle", Float) = 0

		_VerticalOffset ("_VerticalOffset", Float) = 1
		_HorizontalOffset ("_HorizontalOffset", Float) = 1

		_Amplitude ("Amplitude", Float) = 1

		_Frequency ("Frequency", Float) = 1

	}
	 
	SubShader {
	 
		ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings
	 
	 	Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			//we include "UnityCG.cginc" to use the appdata_img struct

			float _XAxis;
			float _VerticalOffset;
			float _HorizontalOffset;
			float _Amplitude;
			float _Frequency;

			struct v2f 
			{
				float4 pos : POSITION;
				half2 uv : TEXCOORD0;
			};
	   
			//Our Vertex Shader
			v2f vert (appdata_img v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
				return o;
			}

			sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders

			//Our Fragment Shader
			fixed4 frag (v2f i) : COLOR
			{

				float2 uv = float2(0,0);

				if (_XAxis == 1)
				{
					uv = float2(i.uv.x, i.uv.y +  ( (_Amplitude *  sin((i.pos.x / _Frequency) + _HorizontalOffset)) + _VerticalOffset) );	
				}
				else
				{
					uv = float2(i.uv.x +  ( (_Amplitude *  sin((i.pos.y / _Frequency) + _VerticalOffset)) + _HorizontalOffset),i.uv.y );
				}

				
				fixed4 orgCol = tex2D(_MainTex, uv); //Get the orginal rendered color




				return orgCol;
			}
			ENDCG
		}
	}
	
	FallBack "Diffuse"
}
