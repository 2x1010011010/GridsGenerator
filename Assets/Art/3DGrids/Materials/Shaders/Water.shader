Shader "Custom/WaterURP_RippleOnly"
{
    Properties
    {
        _ShallowColor ("Shallow Color", Color) = (0.2,0.6,0.8,0.6)
        _DeepColor ("Deep Color", Color) = (0.0,0.15,0.3,0.8)

        _NormalMap1 ("Normal Map 1", 2D) = "bump" {}
        _NormalMap2 ("Normal Map 2", 2D) = "bump" {}

        _RippleSpeed1 ("Ripple Speed 1", Float) = 0.05
        _RippleSpeed2 ("Ripple Speed 2", Float) = -0.03
        _RippleStrength ("Ripple Strength", Range(0,1)) = 0.15

        _DistortionStrength ("UV Distortion", Range(0,0.1)) = 0.02

        _Smoothness ("Smoothness", Range(0,1)) = 0.9
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 positionWS : TEXCOORD2;
            };

            TEXTURE2D(_NormalMap1);
            SAMPLER(sampler_NormalMap1);

            TEXTURE2D(_NormalMap2);
            SAMPLER(sampler_NormalMap2);

            float4 _ShallowColor;
            float4 _DeepColor;

            float _RippleSpeed1;
            float _RippleSpeed2;
            float _RippleStrength;
            float _DistortionStrength;

            float _Smoothness;

            Varyings vert(Attributes input)
            {
                Varyings output;

                VertexPositionInputs posInputs = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normalInputs = GetVertexNormalInputs(input.normalOS);

                output.positionHCS = posInputs.positionCS;
                output.positionWS = posInputs.positionWS;
                output.normalWS = normalInputs.normalWS;
                output.uv = input.uv;

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // === UV Distortion (лёгкое "дыхание" поверхности) ===
                float2 distortion = float2(
                    sin(input.uv.y * 10 + _Time.y) * _DistortionStrength,
                    cos(input.uv.x * 10 + _Time.y) * _DistortionStrength
                );

                float2 uv1 = input.uv + distortion + _Time.y * _RippleSpeed1;
                float2 uv2 = input.uv - distortion + _Time.y * _RippleSpeed2;

                float3 n1 = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap1, sampler_NormalMap1, uv1));
                float3 n2 = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap2, sampler_NormalMap2, uv2));

                float3 rippleNormal = normalize(n1 + n2);
                rippleNormal.xy *= _RippleStrength;

                float3 normalWS = normalize(input.normalWS + rippleNormal);

                Light mainLight = GetMainLight();
                float3 lightDir = normalize(mainLight.direction);

                float NdotL = saturate(dot(normalWS, -lightDir));
                float3 diffuse = mainLight.color * NdotL;

                float3 viewDir = normalize(_WorldSpaceCameraPos - input.positionWS);
                float fresnel = pow(1 - saturate(dot(normalWS, viewDir)), 5);

                float depthFactor = saturate(input.positionWS.y * 0.05);
                float4 waterColor = lerp(_ShallowColor, _DeepColor, depthFactor);

                waterColor.rgb += fresnel * 0.25;

                float3 finalColor = waterColor.rgb * (0.5 + diffuse);

                return float4(finalColor, waterColor.a);
            }

            ENDHLSL
        }
    }
}