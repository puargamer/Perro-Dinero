//shader based on https://www.youtube.com/watch?v=CYc4z4wYu3Q&t=719s
//modified so it works with cubemaps instead of 2d textures

Shader "Skybox/NightDay"
{
    Properties
    {
        _Texture1("Texture1", Cube) = "white" {}
        _Texture2("Texture2", Cube) = "white" {}
        _Blend("Blend", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float3 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            samplerCUBE _Texture1;
            samplerCUBE _Texture2;
            float _Blend;

            v2f vert(appdata v)
            {
                v2f o;
                o.texcoord = v.vertex.xyz;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float2 ToRadialCoords(float3 coords)
            {
                float3 normalizedCoords = normalize(coords);
                float latitude = acos(normalizedCoords.y);
                float longitude = atan2(normalizedCoords.z, normalizedCoords.x);
                const float2 sphereCoords = float2(longitude, latitude) * float2(0.5 / UNITY_PI, 1.0 / UNITY_PI);
                return float2(0.5, 1.0) - sphereCoords;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 tc = ToRadialCoords(i.texcoord);
                fixed4 tex1 = texCUBE(_Texture1, i.texcoord);
                fixed4 tex2 = texCUBE(_Texture2, i.texcoord);
                return lerp(tex1, tex2, _Blend);
            }
            ENDCG
        }
    }
}