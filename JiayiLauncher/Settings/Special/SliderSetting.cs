﻿using System;
using System.Text.Json.Serialization;

namespace JiayiLauncher.Settings.Special;

public struct SliderSetting
{
	[JsonIgnore] public Range Range { get; }
	public int Value { get; }
	
	public SliderSetting(int min, int max, int value)
	{
		Range = new Range(min, max);
		Value = value;
	}
}