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
﻿
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ColorBlindness : MonoBehaviour
{
	public enum Modes
	{
		Normal,
		Protanopia,
		Protanomaly,
		Deuteranopia,
		Deuteranomaly,
		Tritanopia,
		Tritanomaly,
		Achromatopsia,
		Achromatomaly,
	}

	public Modes mode;

	private Material material;
	private static float[] chValues = new float[] {
		1f,     0f,     0f,      0f,     1f,     0f,      0f,     0f,     1f,     // Normal
		0.567f, 0.433f, 0f,      0.558f, 0.442f, 0f,      0f,     0.242f, 0.758f, // Protanopia
		0.817f, 0.183f, 0f,      0.333f, 0.667f, 0f,      0f,     0.125f, 0.875f, // Protanomaly
		0.625f, 0.375f, 0f,      0.7f,   0.3f,   0f,      0f,     0.3f,   0.7f,   // Deuteranopia
		0.8f,   0.2f,   0f,      0.258f, 0.742f, 0f,      0f,     0.142f, 0.858f, // Deuteranomaly
		0.95f,  0.05f,  0f,      0f,     0.433f, 0.567f,  0f,     0.475f, 0.525f, // Tritanopia
		0.967f, 0.033f, 0f,      0f,     0.733f, 0.267f,  0f,     0.183f, 0.817f, // Tritanomaly
		0.299f, 0.587f, 0.114f,  0.299f, 0.587f, 0.114f,  0.299f, 0.587f, 0.114f, // Achromatopsia
		0.618f, 0.320f, 0.062f,  0.163f, 0.775f, 0.062f,  0.163f, 0.320f, 0.516f, // Achromatomaly
	};

	void Awake()
	{
		material = new Material(Shader.Find("Hidden/ColorBlindness"));
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		material.SetInt("_mode", (int)mode);
		material.SetFloatArray("_chValues", chValues);
		Graphics.Blit(source, destination, material);
	}
}
