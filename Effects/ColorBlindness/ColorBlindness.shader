Shader "Hidden/ColorBlindness"
{
	// Copyright 2016 Richard Hawkins
	//
	// Licensed under the Apache License, Version 2.0 (the "License");
	// you may not use this file except in compliance with the License.
	// You may obtain a copy of the License at
	//
	// 		http://www.apache.org/licenses/LICENSE-2.0
	//
	// Unless required by applicable law or agreed to in writing, software
	// distributed under the License is distributed on an "AS IS" BASIS,
	// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	// See the License for the specific language governing permissions and
	// limitations under the License.

	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_bwBlend ("Black & White blend", Range (0, 1)) = 0
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11, Xbox360, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 xbox360 gles
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _chValues[81];
			uniform int _mode;

			float4 frag(v2f_img i) : COLOR
			{
				int o = 9 * _mode;
				float4 c = tex2D(_MainTex, i.uv);
				float4 result = float4(0.0, 0.0, 0.0, c.a);
				result.r = ((c.r * _chValues[o + 0]) + (c.g * _chValues[o + 1]) + (c.b * _chValues[o + 2]));
				result.g = ((c.r * _chValues[o + 3]) + (c.g * _chValues[o + 4]) + (c.b * _chValues[o + 5]));
				result.b = ((c.r * _chValues[o + 6]) + (c.g * _chValues[o + 7]) + (c.b * _chValues[o + 8]));

				return result;
			}
			ENDCG
		}
	}
}
