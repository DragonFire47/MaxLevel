using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace MaxLevel
{
    class Patches
    {
        [HarmonyPatch(typeof(PLShipComponent), "createHashFromInfo")]
        class CompCreateHashPatch
        {
            static bool Prefix(ref uint __result, ref int inAST, ref int inSubType, ref int inLevel, ref int inSubTypeData0, ref int inVisualSlot)
            {
                uint num = (uint)(inAST & 63);
                uint num2 = (uint)((uint)(inSubType & 63) << 6);
                uint num3 = (uint)((uint)(inLevel & 255) << 12);
                uint num4 = (uint)((uint)(inSubTypeData0 & 63) << 20);
                uint num5 = (uint)((uint)(inVisualSlot & 63) << 26);
                __result = num | num2 | num3 | num4 | num5;
				return false;
			}
        }
        [HarmonyPatch(typeof(PLShipComponent), "getHash")]
        class CompGetHashPatch
        {
            static bool Prefix(PLShipComponent __instance, ref uint __result)
            {
                uint num = (uint)(__instance.ActualSlotType & (ESlotType)63);
                uint num2 = (uint)((uint)(__instance.SubType & 63) << 6);
                uint num3 = (uint)((uint)(__instance.Level & 255) << 12);
                uint num4 = ((uint)__instance.SubTypeData & 63U) << 20;
                uint num5 = (uint)((uint)(__instance.VisualSlotType & (ESlotType)63) << 26);
                __result = num | num2 | num3 | num4 | num5;
				return false;
			}
        }
        [HarmonyPatch(typeof(PLShipComponent), "CreateShipComponentFromHash")]
        class CompCreateCompFromHashPatch
        {
			static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
				List<CodeInstruction> instructionList = instructions.ToList();

				instructionList[13].opcode = OpCodes.Ldc_I4;
				instructionList[13].operand = 255;
				instructionList[17].operand = (sbyte)20;
				instructionList[23].operand = (sbyte)26;

				return instructionList.AsEnumerable();
			}
		}
		[HarmonyPatch(typeof(PLPawnItem), "getHash")]
		class ItemGetHashPatch
        {
			static bool Prefix(PLPawnItem __instance, ref uint __result)
            {
				uint num = (uint)(__instance.PawnItemType & (EPawnItemType)63);
				uint num2 = (uint)((uint)(__instance.SubType & 63) << 6);
				uint num3 = (uint)((uint)(__instance.Level & 1048575) << 12);
				__result = num | num2 | num3;
				return false;
            }
        }
		[HarmonyPatch(typeof(PLPawnItem), "GetPawnInfoFromHash")]
		class ItemGetPawnInfoPatch
        {
			static bool Prefix(int inHash, out uint actualSlotTypePart, out uint subTypePart, out uint levelPart)
            {
				actualSlotTypePart = (uint)(inHash & 63);
				subTypePart = ((uint)inHash >> 6 & 63U);
				levelPart = ((uint)inHash >> 12 & 1048575U);
				return false;
            }
        }
	}
}
