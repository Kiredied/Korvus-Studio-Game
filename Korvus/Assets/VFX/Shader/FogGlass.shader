Shader "Custom/FogGlass"
{
    Properties
    {
        _NoiseTex ("Noise (tileable)", 2D) = "gray" {}
        _FogColor ("Fog Color", Color) = (0.5, 0.5, 0.5, 1)
        _NoiseScale ("Noise Scale", Float) = 2
        _Threshold ("Threshold", Range(0,1)) = 0.35
        _Contrast ("Contrast", Float) = 3
        _MaxAlpha ("Max Alpha", Range(0,1)) = 0.6
        _ScrollOffset ("Scroll Offset (set by script)", Vector) = (0,0,0,0)
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name "FogGlass"
            Tags { "LightMode" = "UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _FogColor;
                float _NoiseScale;
                float _Threshold;
                float _Contrast;
                float _MaxAlpha;
                float4 _ScrollOffset;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 flow = _ScrollOffset.xy;
                float2 uv = IN.uv * _NoiseScale;

                // Two layers moving at different speeds for a more layered, less uniform look
                float n1 = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, uv + flow).r;
                float n2 = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, uv * 0.5 + flow * 0.5 + 0.37).r;
                float n = lerp(n1, n2, 0.5);

                float a = saturate((n - _Threshold) * _Contrast) * _MaxAlpha;
                return half4(_FogColor.rgb, a);
            }
            ENDHLSL
        }
    }
}