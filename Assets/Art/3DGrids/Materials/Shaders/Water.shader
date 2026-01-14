Shader "Custom/URP/FastWorldWater"
{
    Properties
    {
        _WaterColor ("Water Color", Color) = (0.05, 0.35, 0.55, 1.0)

        _WaveAmplitude ("Wave Amplitude", Float) = 0.03
        _WaveFrequency ("Wave Frequency", Float) = 0.6
        _WaveSpeed ("Wave Speed", Float) = 0.6
        
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalTiling ("Normal Tiling (World)", Float) = 0.25
        _NormalStrength ("Normal Strength", Float) = 1.0
        
        _FresnelPower ("Fresnel Power", Float) = 3.0
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Geometry"
            "RenderType"="Transparent"
        }

        ZWrite On
        Blend Off
        Cull Back

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos    : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 viewDir     : TEXCOORD2;
            };

            float4 _WaterColor;

            float _WaveAmplitude;
            float _WaveFrequency;
            float _WaveSpeed;

            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            float _NormalTiling;
            float _NormalStrength;

            float _FresnelPower;

            Varyings vert (Attributes v)
            {
                Varyings o;

                float3 worldPos = TransformObjectToWorld(v.positionOS.xyz);

                float t = _Time.y * _WaveSpeed;
                float wave = sin((worldPos.x + worldPos.z) * _WaveFrequency + t);

                worldPos.y += wave * _WaveAmplitude;

                o.positionHCS = TransformWorldToHClip(worldPos);
                o.worldPos = worldPos;
                o.worldNormal = TransformObjectToWorldNormal(v.normalOS);
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);

                return o;
            }
            
            float4 frag (Varyings i) : SV_Target
            {
                float2 uv =
                    i.worldPos.xz * _NormalTiling +
                    _Time.y * float2(0.04, 0.03);

                float3 normalTex = UnpackNormal(
                    SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, uv)
                );

                normalTex.xy *= _NormalStrength;

                float3 normal =
                    normalize(i.worldNormal + normalTex);

                float fresnel =
                    pow(1.0 - saturate(dot(i.viewDir, normal)), _FresnelPower);

                float3 color =
                    lerp(_WaterColor.rgb, _WaterColor.rgb * 1.3, fresnel);

                return float4(color, 1.0);
            }

            ENDHLSL
        }
    }
}
