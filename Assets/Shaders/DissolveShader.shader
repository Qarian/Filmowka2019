Shader "Custom/Dissolve" {

	Properties 
	{
		_Min("Min height", float) = 0

		_Max("Max height", float) = 1

		_Dissolve("Dissolve", Range(0,2)) = 0

		_EmissonPow("EmissionPower",float) = 1

		_EmissionWidth("Emission Width",float) = 1

		_MainTex("Maintex",2D) = "white" {}

		_NoiseTex("Noisetex",2D) = "grey" {}

		_NoiseContrast("Noise Constrast", Range(0,1)) = 1

		_EmissionColorUp("Emmision Up",Color) = (1,1,1,1)

		_EmissionColorDown("Emmision Down",Color) = (1,1,1,1)

		_EmissionStrength("Emission strength", float) = 1
		
		[HDR] _EmissionTextureColor("Emission texture color",Color) = (1,1,1,1)
		

	}

	SubShader
	{
	
		CGPROGRAM
		#pragma surface surf Lambert

		struct Input{
			float3 worldPos;
			float2 uv_MainTex;
			float2 uv_NoiseTex;
		};

		sampler2D _MainTex;
		sampler2D _NoiseTex;

		float _Max;
		float _Min;
		float _Dissolve;
		float _EmissonPow;
		float _EmissionWidth;
		float _NoiseContrast;
		float _EmissionStrength;

		fixed4 _EmissionColorDown;
		fixed4 _EmissionColorUp;
		fixed4 _EmissionTextureColor;
		 



		void surf(in Input IN, inout SurfaceOutput OUT)
		{

			float3 objPos = IN.worldPos ;
			float height = objPos.y;
			float rescaledHeight = ( height - _Min) / (_Max - _Min);
			float noise = tex2D(_NoiseTex,IN.uv_NoiseTex).r;
			float dissolveValue = (rescaledHeight + noise * _NoiseContrast)/( 1 + _NoiseContrast);

			if( _Dissolve > dissolveValue) discard;

			float distanceToDissolve = saturate (  ( dissolveValue - _Dissolve ) *_EmissionWidth);
			float revDistanceToDissolve = 1-distanceToDissolve;

			float3 emissionColor = lerp(_EmissionColorDown,_EmissionColorUp,rescaledHeight);

			float emi= pow(revDistanceToDissolve,_EmissonPow);

			float3 col = tex2D(_MainTex,IN.uv_MainTex).rgb;

		//	OUT.Emission = dissolveValue*emissionColor;
			OUT.Albedo = col;
			OUT.Emission = emi * emissionColor *_EmissionStrength + tex2D(_MainTex,IN.uv_MainTex) * _EmissionTextureColor;
		
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}
