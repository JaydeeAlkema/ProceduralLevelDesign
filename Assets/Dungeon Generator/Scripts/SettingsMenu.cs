using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
	public DungenSettingsScriptableObjects settings;
	[Space]
	public Toggle randomizeSeedToggle = default;
	public TMP_InputField seedInputField = default;
	[Space]
	public TMP_InputField minRoomSizeWidthInputField = default;
	public TMP_InputField minRoomSizeHeightInputField = default;
	[Space]
	public TMP_InputField maxRoomSizeWidthInputField = default;
	public TMP_InputField maxRoomSizeHeightInputField = default;
	[Space( 20 )]
	public TMP_InputField minPathwayCountInputField = default;
	public TMP_InputField maxPathwayCountInputField = default;
	[Space]
	public TMP_InputField minPathwayLengthInputField = default;
	public TMP_InputField maxPathwayLengthInputField = default;

	public void Generate()
	{
		if( randomizeSeedToggle == true ) settings.randomizeSeed = randomizeSeedToggle;
		else settings.seed = seedInputField.text;

		if( minRoomSizeWidthInputField.text.Length > 0 ) settings.minRoomSize.x = int.Parse( minRoomSizeWidthInputField.text );
		if( minRoomSizeHeightInputField.text.Length > 0 ) settings.minRoomSize.y = int.Parse( minRoomSizeHeightInputField.text );

		if( maxRoomSizeWidthInputField.text.Length > 0 ) settings.maxRoomSize.x = int.Parse( maxRoomSizeWidthInputField.text );
		if( maxRoomSizeHeightInputField.text.Length > 0 ) settings.maxRoomSize.y = int.Parse( maxRoomSizeHeightInputField.text );

		if( minPathwayCountInputField.text.Length > 0 ) settings.minPathwayCount = int.Parse( minPathwayCountInputField.text );
		if( maxPathwayCountInputField.text.Length > 0 ) settings.maxPathwayCount = int.Parse( maxPathwayCountInputField.text );

		if( minPathwayLengthInputField.text.Length > 0 ) settings.minPathwayLength = int.Parse( minPathwayLengthInputField.text );
		if( maxPathwayLengthInputField.text.Length > 0 ) settings.maxPathwayLength = int.Parse( maxPathwayLengthInputField.text );

		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
	}
}
