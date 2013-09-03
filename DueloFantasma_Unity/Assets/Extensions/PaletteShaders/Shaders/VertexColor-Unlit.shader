Shader "Palette Shaders/Vertex Color/Unlit" {
	Properties {
		_ColorCount ("Mixed Color Count", float) = 4
		_PaletteHeight ("Palette Height", float) = 128
		_PaletteTex ("Palette", 2D) = "black" {}
		_DitherSize ("Dither Size (Width/Height)", float) = 8
		_DitherTex ("Dither", 2D) = "black" {}
	}

	SubShader {
		Tags { "IgnoreProjector"="True" "RenderType"="Opaque" }
		LOD 110

		Lighting Off
		BlendOp Max

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "CGIncludes/Dithering.cginc"

			sampler2D _PaletteTex;
			sampler2D _DitherTex;
			float _ColorCount;
			float _PaletteHeight;
			float _DitherSize;

			struct VertexInput {
				float4 position : POSITION;
				float4 color : COLOR;
			};

			struct FragmentInput {
				float4 position : SV_POSITION;
				float4 color : COLOR;
				float4 ditherPos : TEXCOORD1;
			};

			FragmentInput vert(VertexInput i) {
				FragmentInput o;
				o.position = mul(UNITY_MATRIX_MVP, i.position);
				o.color = i.color;
				o.ditherPos = GetDitherPos(i.position, _DitherSize);
				return o;
			}

			fixed4 frag(FragmentInput i) : COLOR {
				return fixed4(GetDitherColor(i.color.rgb, _DitherTex, _PaletteTex,
					_PaletteHeight, i.ditherPos, _ColorCount), 1);
			}
			ENDCG
		}
	}

	Fallback "Unlit/Texture"
}