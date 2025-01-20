Shader "Custom/Background Shader"
{
    Properties
    {
        _Colour1 ("Colour 1", Color) = (1, 0, 0, 1)
        _Colour2 ("Colour 2", Color) = (0, 1, 0, 1)
        _Colour3 ("Colour 3", Color) = (0, 0, 1, 1)
        _Contrast ("Contrast", Range(0, 2)) = 1.0
        _SpinAmount ("Spin Amount", Range(0, 1)) = 0.5
        _CustomTime ("Time", Float) = 0.0
        _SpinTime ("Spin Time", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            // Shader properties
            float4 _Colour1;
            float4 _Colour2;
            float4 _Colour3;
            float _Contrast;
            float _SpinAmount;
            float _CustomTime;
            float _SpinTime;

            // Constants
            #define PIXEL_SIZE_FAC 700.0
            #define SPIN_EASE 0.5

            // Vertex shader
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader
            float4 frag(v2f i) : SV_Target
            {
                // Get screen coordinates
                float2 screen_coords = i.uv * _ScreenParams.xy;
                
                // Calculate pixel size based on screen size
                float pixel_size = length(_ScreenParams.xy) / PIXEL_SIZE_FAC;
                float2 uv = (floor(screen_coords * (1.0 / pixel_size)) * pixel_size - 0.5 * _ScreenParams.xy) / length(_ScreenParams.xy) - float2(0.12, 0.0);
                float uv_len = length(uv);

                // Swirl effect based on spin_CustomTime and spin_amount
                float speed = (_SpinTime * SPIN_EASE * 0.2) + 302.2;
                float new_pixel_angle = (atan2(uv.y, uv.x)) + speed - SPIN_EASE * 20.0 * (1.0 * _SpinAmount * uv_len + (1.0 - 1.0 * _SpinAmount));
                float2 mid = (_ScreenParams.xy / length(_ScreenParams.xy)) / 2.0;
                uv = float2((uv_len * cos(new_pixel_angle) + mid.x), (uv_len * sin(new_pixel_angle) + mid.y)) - mid;

                // Add more paint effect to the UV
                uv *= 30.0;
                speed = _CustomTime * 2.0;
                float2 uv2 = uv + uv.yx;

                // Apply multiple iterations of UV distortion
                for (int i = 0; i < 5; i++) {
                    uv2 += sin(max(uv.x, uv.y)) + uv;
                    uv += 0.5 * float2(cos(5.1123314 + 0.353 * uv2.y + speed * 0.131121), sin(uv2.x - 0.113 * speed));
                    uv -= 1.0 * cos(uv.x + uv.y) - 1.0 * sin(uv.x * 0.711 - uv.y);
                }

                // Apply contrast effect
                float contrast_mod = (0.25 * _Contrast + 0.5 * _SpinAmount + 1.2);
                float paint_res = min(2.0, max(0.0, length(uv) * 0.035 * contrast_mod));
                float c1p = max(0.0, 1.0 - contrast_mod * abs(1.0 - paint_res));
                float c2p = max(0.0, 1.0 - contrast_mod * abs(paint_res));
                float c3p = 1.0 - min(1.0, c1p + c2p);

                // Final color computation
                float4 ret_col = (0.3 / contrast_mod) * _Colour1 + (1.0 - 0.3 / contrast_mod) * (_Colour1 * c1p + _Colour2 * c2p + float4(c3p * _Colour3.rgb, c3p * _Colour1.a));

                return ret_col;
            }
            ENDCG
        }
    }
}
