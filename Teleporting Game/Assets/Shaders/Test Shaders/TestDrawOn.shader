Shader "Custom/TestDrawOn"
{
    Properties
    {
        _WallColor ("Wall Color", Color) = (1,1,1,1)
        _WallTex ("Wall (RGB)", 2D) = "white" {}

		_CrystalColor("Color", Color) = (1,1,1,1)
		_CrystalTex("Crystal (RGB)", 2D) = "white" {}
		_Splat("Splat Map",2D) = "black" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
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

        sampler2D _WallTex;
		sampler2D _CrystalTex;


		sampler2D _Splat;

        struct Input
        {
            float2 uv_WallTex;
			float2 uv_CrystalTex;
			float2 uv_Splat;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _WallColor;
		fixed4 _CrystalColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			half amount = tex2Dlod(_Splat, float4(IN.uv_Splat, 0, 0)).r;
			fixed4 c = lerp(tex2D(_WallTex, IN.uv_WallTex) * _WallColor, tex2D(_CrystalTex, IN.uv_CrystalTex) * _CrystalColor, amount);

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
