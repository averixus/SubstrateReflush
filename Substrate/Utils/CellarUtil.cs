using System;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Substrate.Utils
{
    internal static class CellarUtil
    {
        // Determines if the BlockPos is within a cellar
        // This mirrors the vanilla logic for determining a cellar
        // but unfortunately there seems to be no API that directly asks
        // for if something is a cellar or not, we are just allowed to query
        // the room properties and copy what vanilla does.
        public static bool IsInCellar(ICoreAPI api, BlockPos pos)
        {
            try
            {
                var roomRegistry = api.ModLoader.GetModSystem<RoomRegistry>();
                var room = roomRegistry?.GetRoomForPosition(pos);
                if (room == null) return false;

                return room.ExitCount == 0 && room.NonSkylightCount > 0;
            }
            catch (Exception ex)
            {
                SubstrateModSystem.Logger?.Warning($"Cellar detection failed: {ex.Message}");
                api.Logger.Warning($"Cellar detection failed: {ex.Message}");
                return true;
            }
        }
    }
}
