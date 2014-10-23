using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

class MyEditorScript {
	static string[] SCENES = FindEnabledEditorScenes();
	
	static string APP_NAME = "Animin";
	static string TARGET_DIR = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Documents/AniminBuild";

	[MenuItem ("Custom/Build iOS/Release")]
	static void PerformiOSBuild()
	{
		string target_dir = APP_NAME;
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.iPhone, BuildOptions.None);
		CorrectBundleID ();
	}

	[MenuItem ("Custom/Build iOS/Dev")]
	static void PerformiOSBuildDev()
	{
		string target_dir = APP_NAME;
		GenericBuild (SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.iPhone, BuildOptions.Development);
		CorrectBundleID ();
	}
	
	private static string[] FindEnabledEditorScenes() {
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled) continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}
	
	static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes,target_dir,build_target,build_options);
		if (res.Length > 0) {
			throw new Exception("BuildPlayer failure: " + res);
		}
	}

	static void CorrectBundleID()
	{
		string file = TARGET_DIR + "/" + APP_NAME + "/Info.plist";
		string text = File.ReadAllText(file);
		text = text.Replace(@"com.animin.${PRODUCT_NAME}", @"com.do.dog");
		File.WriteAllText(file, text);

	}
}