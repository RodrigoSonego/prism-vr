Shader "Custom/Laser"
{
    Properties
    {
        _Color ("_Color", Color) = (1.0, 1.0, 1.0, 1.0)
        //_MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha One
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = _Color;
                float dist = 1.0 - abs(i.uv.y * 2.0 - 1.0);
                fixed4 col = fixed4(dist, 0, 0, _Color.a);

                float blurred = smoothstep(0.0, 0.3, dist - 0.2);

                dist -= 0.4;
                return fixed4( blurred , 0, 0, 1);

                col *= _Color;
                col.rgb *= _Color.a;
                return col;
            }
            ENDCG
        }
    }
}
