using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Drones
{
    public enum DroneCategory
    {
        Undefined = 0,
        Small = 1,
        Medium = 2,
        Large = 3

    }
    public static class DroneCategoryExtensions
    {
        public static string ToString(this DroneCategory category)
        {
            switch (category)
            {
                case DroneCategory.Undefined:
                    return "Undefined";
                case DroneCategory.Small:
                    return "Small";
                case DroneCategory.Medium:
                    return "Medium";
                case DroneCategory.Large:
                    return "Large";
                default:
                    return "Unknown";
            }
        }
    }
}
