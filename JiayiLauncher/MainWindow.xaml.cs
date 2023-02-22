﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Blazored.Modal;
using JiayiLauncher.Features.Mods;
using JiayiLauncher.Settings;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Extensions.DependencyInjection;

namespace JiayiLauncher;

public partial class MainWindow
{
	[DllImport("dwmapi.dll", PreserveSig = true)]
	private static extern int DwmSetWindowAttribute(nint hWnd, int attr, ref bool attrValue, int attrSize);
	
	public MainWindow()
	{
		InitializeComponent();
		
		SourceInitialized += DarkTitlebar;

		var services = new ServiceCollection();
		services.AddWpfBlazorWebView();
		services.AddBlazoredModal();
#if DEBUG
		services.AddBlazorWebViewDeveloperTools();
#endif
		Resources.Add("services", services.BuildServiceProvider());
		
		// startup stuff
		JiayiSettings.Load();
		if (JiayiSettings.Instance!.ModCollectionPath != string.Empty)
		{
			ModCollection.Load(JiayiSettings.Instance.ModCollectionPath.Value);
		}
	}

	private void DarkTitlebar(object? sender, EventArgs e)
	{
		var windowHelper = new WindowInteropHelper(this);
		var value = true;
		DwmSetWindowAttribute(windowHelper.Handle, 20, ref value, Marshal.SizeOf(value));
	}

	private void ChangeColor(object? sender, BlazorWebViewInitializedEventArgs e)
	{
		e.WebView.DefaultBackgroundColor = Color.FromArgb(15, 15, 15);
	}
}