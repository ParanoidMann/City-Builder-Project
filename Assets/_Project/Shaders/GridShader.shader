Shader "Custom/GridShader" {
	Properties{
		_GridThickness("Grid Thickness", Float) = 0.1
		_GridSpacing("Grid Spacing", Float) = 0.1
		_GridColor("Grid Color", Color) = (0.5, 1.0, 1.0, 1.0)
		_BaseColor("Base Color", Color) = (0.0, 0.0, 0.0, 0.0)
	}

	SubShader{
		Tags{ "Queue" = "Transparent" }

		Pass{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			// Define the vertex and fragment shader functions
			#pragma vertex vert
			#pragma fragment frag

			// Access Shaderlab properties
			uniform float _GridThickness;
			uniform float _GridSpacing;
			uniform float4 _GridColor;
			uniform float4 _BaseColor;

			// Input into the vertex shader
			struct vertexInput {
				float4 vertex : POSITION;
			};

			// Output from vertex shader into fragment shader
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 worldPos : TEXCOORD0;
			};

			// VERTEX SHADER
			vertexOutput vert(vertexInput input) {
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				// Calculate the world position coordinates to pass to the fragment shader
				output.worldPos = mul(unity_ObjectToWorld, input.vertex);

				output.worldPos.x -= _GridThickness / 2;
				output.worldPos.z -= _GridThickness / 2;

 				return output;
			}

			// FRAGMENT SHADER
			float4 frag(vertexOutput input) : COLOR{
				if (frac(input.worldPos.x / _GridSpacing) < _GridThickness || frac(input.worldPos.z / _GridSpacing) < _GridThickness) {
					return _GridColor;
				}
				else {
					return _BaseColor;
				}
			}
			ENDCG
		}
	}
}