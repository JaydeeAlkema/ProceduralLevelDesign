using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor( typeof( DungenRoomFirst ) )]
public class DungenRoomFirstCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		DungenRoomFirst dungenRoomFirst = ( DungenRoomFirst )target;

		if( GUILayout.Button( "Generate Dungeon" ) )
		{
			ClearConsole();
			dungenRoomFirst.Generate();
		}

		if( GUILayout.Button( "Clear Dungeon" ) )
		{
			ClearConsole();
			dungenRoomFirst.ClearDungeon();
		}
	}

	public static void ClearConsole()
	{
		var assembly = Assembly.GetAssembly( typeof( SceneView ) );
		var type = assembly.GetType( "UnityEditor.LogEntries" );
		var method = type.GetMethod( "Clear" );
		method.Invoke( new object(), null );
	}
}
