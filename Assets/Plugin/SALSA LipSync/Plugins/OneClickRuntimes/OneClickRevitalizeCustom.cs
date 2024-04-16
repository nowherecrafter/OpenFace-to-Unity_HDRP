using UnityEngine;

namespace CrazyMinnow.SALSA.OneClicks
{
	public class OneClickCC4RevitalizeEdition : OneClickBase
	{
		/// <summary>
		/// RELEASE NOTES:
		/// 2023-01-21: Updated shapes and movement to support a wider
		///			range of CC4 models and provide better dynamics.
		///		- Switched from 'Teeth02' jaw movement to 'JawRoot'. Creates
		///			much more realistic and dynamic mouth/facial movements.
		///		- Should support CC4 models exported with 60 or 140 blendshape
		///			options.
		/// 2022-07-12: Initial release to support the changes in CC4 models.
		///		NOTE: CC4 models no longer have LR-inclusive shapes and require
		///			more shape components to create the same shape as CC3
		///			models used. The component count is exacerbated when
		///			facial hair or brow overlays are used. Feel free to modify
		///			this component setup to your liking/needs.
		/// ==========================================================================
		/// PURPOSE: This script provides simple, simulated lip-sync input to the
		///		Salsa component from text/string values. For the latest information
		///		visit crazyminnowstudio.com.
		/// ==========================================================================
		/// DISCLAIMER: While every attempt has been made to ensure the safe content
		///		and operation of these files, they are provided as-is, without
		///		warranty or guarantee of any kind. By downloading and using these
		///		files you are accepting any and all risks associated and release
		///		Crazy Minnow Studio, LLC of any and all liability.
		/// ==========================================================================
		/// </summary>
		public static void Setup(GameObject gameObject, AudioClip clip)
		{
			////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//	SETUP Requirements:
			//		use NewExpression("expression name") to start a new viseme/emote expression.
			//		use AddShapeComponent to add blendshape configurations, passing:
			//			- string array of shape names to look for.
			//			  : string array can be a single element.
			//			  : string array can be a single regex search string.
			//			    note: useRegex option must be set true.
			//			- optional string name prefix for the component.
			//			- optional blend amount (default = 1.0f).
			//			- optional regex search option (default = false).

			Init();

			#region SALSA-Configuration
			NewConfiguration(OneClickConfiguration.ConfigType.Salsa);
			{
				////////////////////////////////////////////////////////
				// SMR regex searches (enable/disable/add as required).
				AddSmrSearch("^CC_(base|game)_Body.*$");
				AddSmrSearch("^CC_(base|game)_Tongue.*$");
				AddSmrSearch("^Chin_.*$");

				////////////////////////////////////////////////////////
				// Adjust SALSA settings to taste...
				// - data analysis settings
				autoAdjustAnalysis = true;
				autoAdjustMicrophone = false;
				audioUpdateDelay = 0.095f;
				// - advanced dynamics settings
				loCutoff = 0.015f;
				hiCutoff = 0.75f;
				useAdvDyn = true;
				advDynPrimaryBias = 0.50f;
				useAdvDynJitter = false;
				advDynJitterAmount = 0.10f;
				advDynJitterProb = 0.20f;
				advDynSecondaryMix = 0.0f;
				emphasizerTrigger = 0.0f;

				////////////////////////////////////////////////////////
				// Viseme setup...

				NewExpression("w");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(-0.01266399f, 0.02537677f, 5.429312E-05f),
					new Quaternion(-5.934526E-05f, 4.134326E-05f, -0.7204347f, 0.6935228f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Tight_O" }, 0.11f, 0f, 0.06f, "V_Tight_O", 0.6f, true);
				AddShapeComponent(new[] { "Mouth_Drop_Lower" }, 0.11f, 0f, 0.06f, "Mouth_Drop_Lower", 0.6f, true);
				AddShapeComponent(new[] { "Tongue_in" }, 0.11f, 0f, 0.06f, "Tongue_in", 0.30f, true);

				NewExpression("f");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(-0.01266399f, 0.02537677f, 5.429312E-05f),
					new Quaternion(-5.949008E-05f, 4.119518E-05f, -0.7229443f, 0.6909064f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Dental_Lip" }, 0.11f, 0f, 0.06f, "V_Dental_Lip", 1f, true);
				AddShapeComponent(new[] { "Jaw_Open" }, 0.11f, 0f, 0.06f, "Jaw_Open", 0.08f, true);
				AddShapeComponent(new[] { "Tongue_in" }, 0.11f, 0f, 0.06f, "Tongue_in", 0.20f, true);

				NewExpression("t");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(-0.01266399f, 0.02537677f, 5.429312E-05f),
					new Quaternion(-5.924235E-05f, 4.159705E-05f, -0.717305f, 0.6967593f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Tight_O" }, 0.11f, 0f, 0.06f, "V_Tight_O", 0.31f, true);
				AddShapeComponent(new[] { "Mouth_Up_Upper_L" }, 0.11f, 0f, 0.06f, "Mouth_Up_Upper_L", 0.35f, true);
				AddShapeComponent(new[] { "Mouth_Up_Upper_R" }, 0.11f, 0f, 0.06f, "Mouth_Up_Upper_R", 0.35f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_L" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_L", 0.62f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_R" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_R", 0.62f, true);
				AddShapeComponent(new[] { "Tongue_Up" }, 0.11f, 0f, 0.06f, "Tongue_Up", 0.60f, true);
				AddShapeComponent(new[] { "V_Tongue_Out" }, 0.11f, 0f, 0.06f, "V_Tongue_Out", 0.254f, true);
				AddShapeComponent(new[] { "Mouth_Smile_L" }, 0.11f, 0f, 0.06f, "Mouth_Smile_L", 0.33f, true);
				AddShapeComponent(new[] { "Mouth_Smile_R" }, 0.11f, 0f, 0.06f, "Mouth_Smile_R", 0.33f, true);

				NewExpression("th");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(-0.01266399f, 0.02537677f, 5.429312E-05f),
					new Quaternion(-5.964188E-05f, 4.094E-05f, -0.7268888f, 0.6867551f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "Mouth_Dimple_L" }, 0.11f, 0f, 0.06f, "Mouth_Dimple_L", 0.18f, true);
				AddShapeComponent(new[] { "Mouth_Dimple_R" }, 0.11f, 0f, 0.06f, "Mouth_Dimple_R", 0.18f, true);
				AddShapeComponent(new[] { "Mouth_Up_Upper_L" }, 0.11f, 0f, 0.06f, "Mouth_Up_Upper_L", 0.15f, true);
				AddShapeComponent(new[] { "Mouth_Up_Upper_R" }, 0.11f, 0f, 0.06f, "Mouth_Up_Upper_R", 0.15f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_L" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_L", 0.45f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_R" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_R", 0.45f, true);
				AddShapeComponent(new[] { "V_Tongue_Raise" }, 0.11f, 0f, 0.06f, "V_Tongue_Raise", 0.60f, true);
				AddShapeComponent(new[] { "V_Tongue_Out" }, 0.11f, 0f, 0.06f, "V_Tongue_Out", 0.50f, true);
				AddShapeComponent(new[] { "Tongue_Wide" }, 0.11f, 0f, 0.06f, "Tongue_Wide", 0.5f, true);

				NewExpression("ow");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(-0.01266399f, 0.02537677f, 5.429312E-05f),
					new Quaternion(-6.123816E-05f, 3.863869E-05f, -0.7511058f, 0.6601818f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Tight_O" }, 0.11f, 0f, 0.06f, "V_Tight_O", 0.6f, true);
				AddShapeComponent(new[] { "V_Lip_Open" }, 0.11f, 0f, 0.06f, "V_Lip_Open", 0.6f, true);
				AddShapeComponent(new[] { "Mouth_Up_Upper_L" }, 0.11f, 0f, 0.06f, "Mouth_Up_Upper_L", 0.15f, true);
				AddShapeComponent(new[] { "Mouth_Up_Upper_R" }, 0.11f, 0f, 0.06f, "Mouth_Up_Upper_R", 0.15f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_L" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_L", 0.5f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_R" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_R", 0.5f, true);
				AddShapeComponent(new[] { "Tongue_in" }, 0.11f, 0f, 0.06f, "Tongue_in", 0.30f, true);

				NewExpression("ee");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(-0.01266399f, 0.02537677f, 5.429312E-05f),
					new Quaternion(-5.947331E-05f, 4.123336E-05f, -0.7246742f, 0.6890916f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Dental_Lip" }, 0.11f, 0f, 0.06f, "V_Dental_Lip", 0.57f, true);
				AddShapeComponent(new[] { "V_Wide" }, 0.11f, 0f, 0.06f, "V_Wide", 0.9f, true);
				AddShapeComponent(new[] { "Mouth_Dimple_L" }, 0.11f, 0f, 0.06f, "Mouth_Dimple_L", 0.33f, true);
				AddShapeComponent(new[] { "Mouth_Dimple_R" }, 0.11f, 0f, 0.06f, "Mouth_Dimple_R", 0.33f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_L" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_L", 0.9f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_R" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_R", 0.9f, true);
				AddShapeComponent(new[] { "Tongue_up" }, 0.11f, 0f, 0.06f, "Tongue_up", 0.70f, true);

				NewExpression("oo");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(-0.01266399f, 0.02537677f, 5.429312E-05f),
					new Quaternion(-6.339697E-05f, 3.486871E-05f, -0.789081f, 0.6142891f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Lip_Open" }, 0.11f, 0f, 0.06f, "V_Lip_Open", 0.545f, true);
				AddShapeComponent(new[] { "V_Affricate" }, 0.11f, 0f, 0.06f, "V_Affricate", 0.552f, true);
				AddShapeComponent(new[] { "Tongue_Down" }, 0.11f, 0f, 0.06f, "Tongue_Down", 0.79f, true);
				AddShapeComponent(new[] { "V_Wide" }, 0.11f, 0f, 0.06f, "V_Wide", 0.552f, true);
			}
			#endregion // SALSA-configuration

			#region EmoteR-Configuration
			NewConfiguration(OneClickConfiguration.ConfigType.Emoter);
			{
				////////////////////////////////////////////////////////
				// SMR regex searches (enable/disable/add as required).
				AddSmrSearch("^CC_(base|game)_Body$");
				AddSmrSearch("^Mustache.*$");
				AddSmrSearch("^Brows.*$");
				AddSmrSearch("^Beard.*$");


				useRandomEmotes = false;
				isChancePerEmote = true;
				numRandomEmotesPerCycle = 0;
				randomEmoteMinTimer = 1f;
				randomEmoteMaxTimer = 2f;
				randomChance = 0.5f;
				useRandomFrac = false;
				randomFracBias = 0.5f;
				useRandomHoldDuration = false;
				randomHoldDurationMin = 0.1f;
				randomHoldDurationMax = 0.5f;

				////////////////////////////////////////////////////////
				// Emote setup...

				NewExpression("Moue");
				AddEmoteFlags(false, false, false, 1f);
				AddShapeComponent(new[] { "^Brow_Drop_L.*$" }, 0.6f, 0.1f, 0.6f, "Brow_Drop_L", 0.80f, true);
				AddShapeComponent(new[] { "^Brow_Drop_R.*$" }, 0.6f, 0.1f, 0.6f, "Brow_Drop_R", 0.80f, true);
				AddShapeComponent(new[] { "^Brow_Raise_Outer_L.*$" }, 0.6f, 0.1f, 0.6f, "Brow_Raise_Outer_L", 1f, true);
				AddShapeComponent(new[] { "^Brow_Raise_Outer_R.*$" }, 0.6f, 0.1f, 0.6f, "Brow_Raise_Outer_R", 1f, true);
				AddShapeComponent(new[] { "^Eye_Wide_L.*$" }, 0.6f, 0.1f, 0.6f, "Eye_Wide_L", 0.20f, true);
				AddShapeComponent(new[] { "^Eye_Wide_R.*$" }, 0.6f, 0.1f, 0.6f, "Eye_Wide_R", 0.20f, true);
				AddShapeComponent(new[] { "^Nose_Sneer_L.*$" }, 0.6f, 0.1f, 0.6f, "Nose_Sneer_L", 0.40f, true);
				AddShapeComponent(new[] { "^Nose_Sneer_R.*$" }, 0.6f, 0.1f, 0.6f, "Nose_Sneer_R", 0.40f, true);
				AddShapeComponent(new[] { "^Nose_Crease_L.*$" }, 0.6f, 0.1f, 0.6f, "Nose_Crease_L", 0.30f, true);
				AddShapeComponent(new[] { "^Nose_Crease_R.*$" }, 0.6f, 0.1f, 0.6f, "Nose_Crease_R", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Frown_L.*$" }, 0.6f, 0.1f, 0.6f, "Mouth_Frown_L", 0.70f, true);
				AddShapeComponent(new[] { "^Mouth_Frown_R.*$" }, 0.6f, 0.1f, 0.6f, "Mouth_Frown_R", 0.70f, true);
				AddShapeComponent(new[] { "^Mouth_Tighten_L.*$" }, 0.6f, 0.1f, 0.6f, "Mouth_Tighten_L", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Tighten_R.*$" }, 0.6f, 0.1f, 0.6f, "Mouth_Tighten_R", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Up.*$" }, 0.6f, 0.1f, 0.6f, "Mouth_Up", 0.40f, true);
				AddShapeComponent(new[] { "^Brow_Raise_Inner_L.*$" }, 0.6f, 0.1f, 0.6f, "Brow_Raise_Inner_L", -1f, true);
				AddShapeComponent(new[] { "^Brow_Raise_Inner_R.*$" }, 0.6f, 0.1f, 0.6f, "Brow_Raise_Inner_R", -1f, true);
				AddShapeComponent(new[] { "^Brow_Compress_L.*$" }, 0.6f, 0.1f, 0.6f, "Brow_Compress_L", 1.5f, true);
				AddShapeComponent(new[] { "^Brow_Compress_R.*$" }, 0.6f, 0.1f, 0.6f, "Brow_Compress_R", 1.5f, true);

				NewExpression("Doubt_Old");
				AddEmoteFlags(false, false, false, 1f);
				AddShapeComponent(new[] { "^Brow_Raise_Inner_R.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Raise_Inner_R", 0.32f, true);
				AddShapeComponent(new[] { "^Brow_Raise_Outer_L.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Raise_Outer_L", 0.50f, true);
				AddShapeComponent(new[] { "^Brow_Raise_Outer_R.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Raise_Outer_R", 0.80f, true);
				AddShapeComponent(new[] { "^Brow_Drop_L.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Drop_L", 1f, true);
				AddShapeComponent(new[] { "^Brow_Compress_L.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Compress_L", 0.50f, true);
				AddShapeComponent(new[] { "^Eye_Squint_L.*$" }, 0.6f, 0.1f, 0.15f, "Eye_Squint_L", 0.30f, true);
				AddShapeComponent(new[] { "^Eye_Squint_R.*$" }, 0.6f, 0.1f, 0.15f, "Eye_Squint_R", 0.30f, true);
				AddShapeComponent(new[] { "^Nose_Sneer_L.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Sneer_L", 0.35f, true);
				AddShapeComponent(new[] { "^Nose_Sneer_R.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Sneer_R", 0.35f, true);
				AddShapeComponent(new[] { "^Nose_Crease_L.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Crease_L", 0.40f, true);
				AddShapeComponent(new[] { "^Nose_Crease_R.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Crease_R", 0.40f, true);
				AddShapeComponent(new[] { "^Cheek_Raise_L.*$" }, 0.6f, 0.1f, 0.15f, "Cheek_Raise_L", 0.20f, true);
				AddShapeComponent(new[] { "^Cheek_Raise_R.*$" }, 0.6f, 0.1f, 0.15f, "Cheek_Raise_R", 0.20f, true);
				AddShapeComponent(new[] { "^Mouth_Frown_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Frown_L", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Frown_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Frown_R", 0.70f, true);
				AddShapeComponent(new[] { "^Mouth_Tighten_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Tighten_L", 0.40f, true);
				AddShapeComponent(new[] { "^Mouth_Tighten_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Tighten_R", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Dimple_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Dimple_R", 0.20f, true);
				AddShapeComponent(new[] { "^Mouth_Push_Lower_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Push_Lower_L", 0.33f, true);
				AddShapeComponent(new[] { "^Mouth_Push_Lower_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Push_Lower_R", 0.38f, true);
				AddShapeComponent(new[] { "^Mouth_R*$" }, 0.6f, 0.1f, 0.15f, "Mouth_R", 0.30f, true);

				NewExpression("Smile");
				AddEmoteFlags(false, false, false, 1f);
				AddShapeComponent(new[] { "^Brow_Raise_Outer_L.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Raise_Outer_L", 0.30f, true);
				AddShapeComponent(new[] { "^Brow_Raise_Outer_R.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Raise_Outer_R", 0.30f, true);
				AddShapeComponent(new[] { "^Brow_Compress_L.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Compress_L", 0.40f, true);
				AddShapeComponent(new[] { "^Brow_Compress_R.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Compress_R", 0.40f, true);
				AddShapeComponent(new[] { "^Eye_Squint_L.*$" }, 0.6f, 0.1f, 0.15f, "Eye_Squint_L", 0.10f, true);
				AddShapeComponent(new[] { "^Eye_Squint_R.*$" }, 0.6f, 0.1f, 0.15f, "Eye_Squint_R", 0.10f, true);
				AddShapeComponent(new[] { "^Nose_Crease_L.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Crease_L", 0.25f, true);
				AddShapeComponent(new[] { "^Nose_Crease_R.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Crease_R", 0.25f, true);
				AddShapeComponent(new[] { "^Cheek_Raise_L.*$" }, 0.6f, 0.1f, 0.15f, "Cheek_Raise_L", 0.30f, true);
				AddShapeComponent(new[] { "^Cheek_Raise_R.*$" }, 0.6f, 0.1f, 0.15f, "Cheek_Raise_R", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Smile_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Smile_L", 0.50f, true);
				AddShapeComponent(new[] { "^Mouth_Smile_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Smile_R", 0.50f, true);
				AddShapeComponent(new[] { "^Mouth_Smile_Sharp_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Smile_Sharp_L", 0.20f, true);
				AddShapeComponent(new[] { "^Mouth_Smile_Sharp_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Smile_Sharp_R", 0.20f, true);
				AddShapeComponent(new[] { "^Mouth_Down.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Down", 0.31f, true);

				NewExpression("Doubt");
				AddEmoteFlags(false, false, false, 1f);
				AddShapeComponent(new[] { "^Brow_Raise_Outer_L.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Raise_Outer_L", 0.70f, true);
				AddShapeComponent(new[] { "^Brow_Raise_Outer_R.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Raise_Outer_R", 0.70f, true);
				AddShapeComponent(new[] { "^Brow_Drop_L.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Drop_L", 1.50f, true);
				AddShapeComponent(new[] { "^Brow_Drop_R.*$" }, 0.6f, 0.1f, 0.15f, "Brow_Drop_R", 1.50f, true);
				AddShapeComponent(new[] { "^Eye_Squint_L.*$" }, 0.6f, 0.1f, 0.15f, "Eye_Squint_L", 0.30f, true);
				AddShapeComponent(new[] { "^Eye_Squint_R.*$" }, 0.6f, 0.1f, 0.15f, "Eye_Squint_R", 0.30f, true);
				AddShapeComponent(new[] { "^Nose_Sneer_L.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Sneer_L", 0.35f, true);
				AddShapeComponent(new[] { "^Nose_Sneer_R.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Sneer_R", 0.35f, true);
				AddShapeComponent(new[] { "^Nose_Crease_L.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Crease_L", 0.40f, true);
				AddShapeComponent(new[] { "^Nose_Crease_R.*$" }, 0.6f, 0.1f, 0.15f, "Nose_Crease_R", 0.40f, true);
				AddShapeComponent(new[] { "^Cheek_Raise_L.*$" }, 0.6f, 0.1f, 0.15f, "Cheek_Raise_L", 0.20f, true);
				AddShapeComponent(new[] { "^Mouth_Frown_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Frown_L", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Frown_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Frown_R", 0.70f, true);
				AddShapeComponent(new[] { "^Mouth_Tighten_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Tighten_L", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Tighten_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Tighten_R", 0.40f, true);
				AddShapeComponent(new[] { "^Mouth_Dimple_R.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Dimple_R", 0.30f, true);
				AddShapeComponent(new[] { "^Mouth_Push_Lower_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Push_Lower_L", 0.33f, true);
				AddShapeComponent(new[] { "^Mouth_Push_Lower_L.*$" }, 0.6f, 0.1f, 0.15f, "Mouth_Push_Lower_L", 0.38f, true);
				AddShapeComponent(new[] { "^Mouth_R*$" }, 0.6f, 0.1f, 0.15f, "Mouth_R", 0.30f, true);
			}
			#endregion // EmoteR-configuration

			DoOneClickiness(gameObject, clip);

			if (selectedObject.GetComponent<SalsaAdvancedDynamicsSilenceAnalyzer>() == null)
				selectedObject.AddComponent<SalsaAdvancedDynamicsSilenceAnalyzer>();

			// Darrin's Tweaks
			salsa.useTimingsOverride = true;
			salsa.globalDurON = 0.125f;
			salsa.globalDurOffBalance = -0.180f;
			salsa.globalNuanceBalance = -0.213f;

			emoter.NumRandomEmphasizersPerCycle = 4;
			emoter.FindEmote("Moue").frac = 1f;

			var silenceAnalyzer = selectedObject.GetComponent<SalsaAdvancedDynamicsSilenceAnalyzer>();
			silenceAnalyzer.silenceThreshold = 0.9f;
			silenceAnalyzer.timingStartPoint = 0.4f;
			silenceAnalyzer.timingEndVariance = 0.7f;
			silenceAnalyzer.silenceSampleWeight = 0.98f;
			silenceAnalyzer.bufferSize = 512;
		}
	}
}

