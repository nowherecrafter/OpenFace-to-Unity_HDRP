using UnityEngine;

namespace CrazyMinnow.SALSA.OneClicks
{
	public class OneClickCC4GameReadyRevitalizeEdition : OneClickBase
	{
		/// <summary>
		/// RELEASE NOTES:
		/// 2023-03-29: Changed SMR searches to capture game or base nomenclature.
		/// 2022-07-12: Initial release to support the changes in CC4 models and
		///		provides a preset for game-ready-builds which have different bones
		///		and blendshape movements.
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
				advDynPrimaryBias = 0.5f;
				useAdvDynJitter = false;
				advDynJitterAmount = 0.1f;
				advDynJitterProb = 0.2f;
				advDynSecondaryMix = 0f;
				emphasizerTrigger = 0f;

				////////////////////////////////////////////////////////
				// Viseme setup...

				NewExpression("w");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(0.02461322f, 0.01624778f, 4.43144E-07f),
					new Quaternion(5.382616E-07f, -6.750596E-05f, 0.9987144f, 0.05069043f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Tight_O" }, 0.11f, 0f, 0.06f, "V_Tight_O", 0.65f, true);
				AddShapeComponent(new[] { "Tongue_In" }, 0.11f, 0f, 0.06f, "Tongue_In", 0.3f, true);

				NewExpression("f");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(0.02461322f, 0.01624778f, 4.43144E-07f),
					new Quaternion(5.931381E-07f, -6.755066E-05f, 0.9989046f, 0.04679359f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Dental_Lip" }, 0.11f, 0f, 0.06f, "V_Dental_Lip", 0.9f, true);
				AddShapeComponent(new[] { "Jaw_Open" }, 0.11f, 0f, 0.06f, "Jaw_Open", 0.08f, true);
				AddShapeComponent(new[] { "Tongue_In" }, 0.11f, 0f, 0.06f, "Tongue_In", 0.2f, true);

				NewExpression("t");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(0.02461322f, 0.01624778f, 4.43144E-07f),
					new Quaternion(3.975474E-07f, -6.749292E-05f, 0.9983438f, 0.05752898f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_L" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_L", 0.4f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_R" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_R", 0.4f, true);
				AddShapeComponent(new[] { "V_Tongue_up" }, 0.11f, 0f, 0.06f, "V_Tongue_up", 0.6f, true);
				AddShapeComponent(new[] { "V_Tongue_Out" }, 0.11f, 0f, 0.06f, "V_Tongue_Out", 0.254f, true);
				AddShapeComponent(new[] { "Mouth_Smile_L" }, 0.11f, 0f, 0.06f, "Mouth_Smile_L", 0.33f, true);
				AddShapeComponent(new[] { "Mouth_Smile_R" }, 0.11f, 0f, 0.06f, "Mouth_Smile_R", 0.33f, true);

				NewExpression("th");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(0.02461322f, 0.01624778f, 4.43144E-07f),
					new Quaternion(-3.75381E-07f, -6.756926E-05f, 0.9982508f, 0.05912095f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "Mouth_Dimple_L" }, 0.11f, 0f, 0.06f, "Mouth_Dimple_L", 0.18f, true);
				AddShapeComponent(new[] { "Mouth_Dimple_R" }, 0.11f, 0f, 0.06f, "Mouth_Dimple_R", 0.18f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_L" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_L", 0.25f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_R" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_R", 0.25f, true);
				AddShapeComponent(new[] { "V_Tongue_Raise" }, 0.11f, 0f, 0.06f, "V_Tongue_Raise", 0.6f, true);
				AddShapeComponent(new[] { "V_Tongue_Out" }, 0.11f, 0f, 0.06f, "V_Tongue_Out", 0.5f, true);
				AddShapeComponent(new[] { "Tongue_Wide" }, 0.11f, 0f, 0.06f, "Tongue_Wide", 0.5f, true);

				NewExpression("ow");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(0.02461322f, 0.01624778f, 4.43144E-07f),
					new Quaternion(-1.328343E-06f, -6.721538E-05f, 0.996443f, 0.08426984f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Tight_O" }, 0.11f, 0f, 0.06f, "V_Tight_O", 0.6f, true);
				AddShapeComponent(new[] { "V_Lip_Open" }, 0.11f, 0f, 0.06f, "V_Lip_Open", 0.6f, true);
				AddShapeComponent(new[] { "Tongue_In" }, 0.11f, 0f, 0.06f, "Tongue_In", 0.3f, true);

				NewExpression("ee");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(0.02461322f, 0.01624778f, 4.43144E-07f),
					new Quaternion(-6.62891E-07f, -6.739418E-05f, 0.9976999f, 0.06778551f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Wide" }, 0.11f, 0f, 0.06f, "V_Wide", 1f, true);
				AddShapeComponent(new[] { "Mouth_Dimple_L" }, 0.11f, 0f, 0.06f, "Mouth_Dimple_L", 0.33f, true);
				AddShapeComponent(new[] { "Mouth_Dimple_R" }, 0.11f, 0f, 0.06f, "Mouth_Dimple_R", 0.33f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_L" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_L", 0.35f, true);
				AddShapeComponent(new[] { "Mouth_Down_Lower_R" }, 0.11f, 0f, 0.06f, "Mouth_Down_Lower_R", 0.35f, true);
				AddShapeComponent(new[] { "V_Tongue_up" }, 0.11f, 0f, 0.06f, "V_Tongue_up", 0.7f, true);

				NewExpression("oo");
				AddBoneComponent("^CC_Base_JawRoot$",
					new TformBase(new Vector3(0.02461322f, 0.01624778f, 4.43144E-07f),
					new Quaternion(-4.154872E-06f, -6.666404E-05f, 0.9936293f, 0.1126976f),
					new Vector3(1f, 1f, 1f)),
					0.11f, 0f, 0.06f,
					"CC_Base_JawRoot",
					false, true, true);
				AddShapeComponent(new[] { "V_Lip_Open" }, 0.11f, 0f, 0.06f, "V_Lip_Open", 0.545f, true);
				AddShapeComponent(new[] { "V_Affricate" }, 0.11f, 0f, 0.06f, "V_Affricate", 0.3f, true);
				AddShapeComponent(new[] { "Tongue_Down" }, 0.11f, 0f, 0.06f, "Tongue_Down", 0.79f, true);
				AddShapeComponent(new[] { "V_Wide" }, 0.11f, 0f, 0.06f, "V_Wide", 0.65f, true);
			}
			#endregion // SALSA-configuration

			#region EmoteR-Configuration
			NewConfiguration(OneClickConfiguration.ConfigType.Emoter);
			{
				////////////////////////////////////////////////////////
				// SMR regex searches (enable/disable/add as required).
				AddSmrSearch("^CC_(base|game)_Body.*$");
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

			//Darrin's Tweaks
			salsa.useTimingsOverride = true;
			salsa.globalDurON = 0.125f;
			salsa.globalDurOffBalance = -0.18f;
			salsa.globalNuanceBalance = -0.213f;

			emoter.NumRandomEmphasizersPerCycle = 4;
			EmoteExpression emote;
			emote = emoter.FindEmote("exasper");
			if (emote != null)
				emote.frac = 0.628f;
			emote = emoter.FindEmote("soften");
			if (emote != null)
				emote.frac = 0.705f;
			emote = emoter.FindEmote("browsUp");
			if (emote != null)
				emote.frac = 0.469f;
			emote = emoter.FindEmote("browUp");
			if (emote != null)
				emote.frac = 0.817f;
			emote = emoter.FindEmote("squint");
			if (emote != null)
				emote.frac = 0.782f;
			emote = emoter.FindEmote("focus");
			if (emote != null)
				emote.frac = 1f;
			emote = emoter.FindEmote("flare");
			if (emote != null)
				emote.frac = 1f;
			emote = emoter.FindEmote("scrunch");
			if (emote != null)
				emote.frac = 0.853f;

			var silenceAnalyzer = selectedObject.GetComponent<SalsaAdvancedDynamicsSilenceAnalyzer>();
			if (silenceAnalyzer)
			{
				silenceAnalyzer.silenceThreshold = 0.9f;
				silenceAnalyzer.timingStartPoint = 0.4f;
				silenceAnalyzer.timingEndVariance = 0.7f;
				silenceAnalyzer.silenceSampleWeight = 0.98f;
				silenceAnalyzer.bufferSize = 512;
			}
		}
	}
}

