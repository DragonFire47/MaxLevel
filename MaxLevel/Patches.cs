using HarmonyLib;

namespace MaxLevel
{
    class Patches
    {
        [HarmonyPatch(typeof(PLShipComponent), "createHashFromInfo")]
        class CompCreateHashPatch
        {
            static void Prefix(ref uint __result, ref int inAST, ref int inSubType, ref int inLevel, ref int inSubTypeData0, ref int inVisualSlot)
            {
                uint num = (uint)(inAST & 63);
                uint num2 = (uint)((uint)(inSubType & 63) << 6);
                uint num3 = (uint)((uint)(inLevel & 15) << 12);
                uint num4 = (uint)((uint)(inSubTypeData0 & 63) << 16);
                uint num5 = (uint)((uint)(inVisualSlot & 63) << 22);
                __result = num | num2 | num3 | num4 | num5;
            }
        }
        [HarmonyPatch(typeof(PLShipComponent), "getHash")]
        class CompGetHashPatch
        {
            static void Prefix(PLShipComponent __instance, ref uint __result)
            {
                uint num = (uint)(__instance.ActualSlotType & (ESlotType)63);
                uint num2 = (uint)((uint)(__instance.SubType & 63) << 6);
                uint num3 = (uint)((uint)(__instance.Level & 15) << 12);
                uint num4 = ((uint)__instance.SubTypeData & 63U) << 16;
                uint num5 = (uint)((uint)(__instance.VisualSlotType & (ESlotType)63) << 22);
                __result = num | num2 | num3 | num4 | num5;
            }
        }
        [HarmonyPatch(typeof(PLShipComponent), "CreateShipComponentFromHash")]
        class CompCreateCompFromHashPatch
        {
            static void Prefix(ref int inHash, ref PLShipComponent __result)
            {
				uint num = (uint)(inHash & 63);
				uint inSubType = (uint)inHash >> 6 & 63U;
				uint inLevel = (uint)inHash >> 12 & 15U;
				uint num2 = (uint)inHash >> 16 & 63U;
				uint visualSlotType = (uint)inHash >> 22 & 63U;
				PLShipComponent plshipComponent = null;
				switch (num)
				{
					case 1U:
						plshipComponent = PLShieldGenerator.CreateShieldGeneratorFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 2U:
						plshipComponent = PLWarpDrive.CreateWarpDriveFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 3U:
						plshipComponent = PLReactor.CreateReactorFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 5U:
						plshipComponent = PLSensor.CreateSensorFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 6U:
						plshipComponent = PLHull.CreateHullFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 7U:
						plshipComponent = PLCPU.CreateCPUFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 8U:
						plshipComponent = PLOxygenGenerator.CreateO2GeneratorFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 9U:
						plshipComponent = PLThruster.CreateThrusterFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 10U:
						plshipComponent = PLTurret.CreateTurretFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 11U:
						plshipComponent = PLMegaTurret.CreateMainTurretFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 16U:
						plshipComponent = PLHullPlating.CreateHullPlatingFromHash((int)inSubType, (int)inLevel, (int)num2);
						break;
					case 17U:
						plshipComponent = PLWarpDriveProgram.CreateWarpDriveProgramFromHash((int)inSubType, (int)inLevel, (short)num2);
						break;
					case 18U:
						plshipComponent = PLVirus.CreateVirusFromHash((int)inSubType, (int)inLevel, (short)num2);
						break;
					case 19U:
						plshipComponent = PLNuclearDevice.CreateNuclearDeviceFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 20U:
						plshipComponent = PLTrackerMissile.CreateTrackerMissileFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 21U:
						plshipComponent = PLScrapCargo.CreateScrap((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 22U:
						plshipComponent = PLDistressSignal.CreateDistressSignalFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 23U:
						plshipComponent = PLMissionShipComponent.CreateMissionComponentFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 24U:
						plshipComponent = PLAutoTurret.CreateAutoTurretFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 25U:
						plshipComponent = PLInertiaThruster.CreateInertiaThrusterFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 26U:
						plshipComponent = PLManeuverThruster.CreateManeuverThrusterFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 27U:
						plshipComponent = PLCaptainsChair.CreateCaptainsChairFromHash((int)inSubType, (int)inLevel, (short)num2);
						break;
					case 28U:
						plshipComponent = PLExtractor.CreateExtractorFromHash((int)inSubType, (int)inLevel, (short)num2);
						break;
					case 30U:
						plshipComponent = PLFBRecipeModule.CreateRecipeFromHash((int)inSubType, (int)inLevel, (short)num2);
						break;
					case 31U:
						plshipComponent = PLBiscuitBombComponent.CreateBiscuitBombFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 32U:
						plshipComponent = PLSensorDish.CreateSensorDishFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
					case 33U:
						plshipComponent = PLCloakingSystem.CreateCloakingSystemFromHash((int)inSubType, (int)inLevel, (int)((short)num2));
						break;
				}
				if (plshipComponent != null)
				{
					plshipComponent.VisualSlotType = (ESlotType)visualSlotType;
					if (plshipComponent.VisualSlotType == ESlotType.E_COMP_NONE)
					{
						plshipComponent.VisualSlotType = plshipComponent.ActualSlotType;
					}
				}
				__result = plshipComponent;
			}
        }
    }
}
