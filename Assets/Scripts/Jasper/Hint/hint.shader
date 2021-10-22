Shader "Hidden/hint"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _bwBlend("Black & White blend", Range(0, 1)) = 0
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
            uniform float _bwBlend;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 finalColor;
                if (abs(col.x - 0.7) < 0.1 && abs(col.y - 1) < 0.1 && abs(col.z - 0) < 0.1)
                {
                    if (_bwBlend < 1.0)
                    {
                        finalColor.rgb = lerp(float3(0.3, 0.3, 0.3), float3(0.6, 0.2, 0.1), _bwBlend);
                        finalColor.w = 1;
                    }
                    else
                    {
                        finalColor = fixed4(0.6, 0.2, 0.1, 1);
                    }
                }
                else
                {
                    float intensity = col.x * 0.33 + col.y * 0.33 + col.z * 0.33;
                    finalColor = fixed4(intensity, intensity, intensity, 1);
                    if (_bwBlend < 1.0)
                    {
                        finalColor.rgb = lerp(col.rgb, finalColor.rgb, _bwBlend);
                    }
                }
                return finalColor;
            }
            ENDCG
        }
    }
}
