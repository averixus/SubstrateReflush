using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Substrate.Blocks;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace Substrate.Patches
{
    [HarmonyPatch(typeof(BlockRequireFertileGround), nameof(BlockRequireFertileGround.CanPlantStay))]
    public class FertilityCheckPatch
    {
        [HarmonyPostfix]
        public static void Postfix(BlockRequireFertileGround __instance, IBlockAccessor blockAccessor, BlockPos pos, ref bool __result)
        {
            if (__instance is not BlockMushroom mushroom) return;
            
            var below = blockAccessor.GetBlockBelow(pos);

            if (below is BlockMultiblock multiblock)
            {
                below = blockAccessor.GetBlockBelow(pos.AddCopy(multiblock.OffsetInv));
            }

            if (below is BlockGrowBed) __result = true;
        }
    }
}
