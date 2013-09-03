Shader "Palette Shaders/Vertex Color/Unlit Simple" {
	Properties {
		_ColorCount ("Mixed Color Count", float) = 4
		_PaletteHeight ("Palette Height", float) = 128
		_PaletteTex ("Palette (Max 4 Mixed Colors)", 2D) = "black" {}
	}

	SubShader {
		Tags { "IgnoreProjector"="True" "RenderType"="Opaque" }
		LOD 110

		Lighting Off

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "CGIncludes/Dithering.cginc"

			sampler2D _PaletteTex;
			float _PaletteHeight;
			float _ColorCount;

			struct VertexInput {
				float4 position : POSITION;
				float4 color : COLOR;
			};

			struct FragmentInput {
				float4 position : SV_POSITION;
				float4 color : COLOR;
				float4 screenPos : TEXCOORD1;
			};

			FragmentInput vert(VertexInput i) {
				FragmentInput o;
				o.position = mul(UNITY_MATRIX_MVP, i.position);
				o.color = i.color;
				o.screenPos = ComputeScreenPos(o.position);
				return o;
			}

			fixed4 frag(FragmentInput i) : COLOR {
				return fixed4(GetDitherColorSimple(i.color.rgb, _PaletteTex,
					_PaletteHeight, i.screenPos, _ColorCount), 1);
			}
			ENDCG
		}
	}

	Fallback "Unlit/Texture"
}