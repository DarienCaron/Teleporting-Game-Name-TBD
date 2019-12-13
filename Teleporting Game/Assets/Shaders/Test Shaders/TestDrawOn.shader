Shader "Custom/TestDrawOn"
{
    Properties
    {
        _WallColor ("Wall Color", Color) = (1,1,1,1)
        _WallTex ("Wall (RGB)", 2D) = "white" {}

		_CrystalColor("Color", Color) = (1,1,1,1)
		_CrystalTex1("Crystal (RGB)", 2D) = "white" {}
		_CrystalTex2("Crystal (RGB)", 2D) = "white" {}
		_CrystalTex3("Crystal (RGB)", 2D) = "white" {}

		[PerRendererData]_Splat("Splat Map",2D) = "black" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

		_BumpMap("Normal Map", 2D) = "bump" {}
		_CrystalBump("Normal Map", 2D) = "bump" {}

		_BumpMapPower("Normal Map Pow", Range(0,2)) = 0.5
		_BumpMapPower2("Normal Map Pow 2", Range(0,2)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
		
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
		#include "UnityStandardUtils.cginc"

        sampler2D _WallTex;
		sampler2D _CrystalTex1;
		sampler2D _CrystalTex2;
		sampler2D _CrystalTex3;


		sampler2D _Splat;

        struct Input
        {
            float2 uv_WallTex;
			float2 uv_CrystalTex1;
			float2 uv_CrystalTex2;
			float2 uv_CrystalTex3;
			float2 uv_Splat;
			float2 uv_BumpMap;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _WallColor;
		fixed4 _CrystalColor;
		sampler2D _BumpMap;
		sampler2D _CrystalBump;


		float _BumpMapPower;
		float _BumpMapPower2;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float3 Mask = tex2D(_Splat, IN.uv_Splat);

			float cMask = min(1.0, Mask.r + Mask.g + Mask.b);

			float4 first = tex2D(_CrystalTex1, IN.uv_CrystalTex1);
			float4 second = tex2D(_CrystalTex2, IN.uv_CrystalTex2);
			float4 third = tex2D(_CrystalTex3, IN.uv_CrystalTex3);



			fixed4 c = tex2D(_WallTex, IN.uv_WallTex) * _WallColor;

			c.rgb = c.rgb * (1 - cMask) + (first * Mask.r) + (second * Mask.g) + (third * Mask.b);

			

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
			
			o.Normal = lerp(UnpackScaleNormal(tex2D(_BumpMap, IN.uv_BumpMap),_BumpMapPower), UnpackScaleNormal(tex2D(_CrystalBump, IN.uv_BumpMap), _BumpMapPower2), Mask.r);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
