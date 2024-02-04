using System.ComponentModel;

namespace RPG
{
    public enum SkillLevel
    {
        [Description("Very Low")]
        VeryLow,
        Low,
        Average,
        High,
        [Description("Very High")]
        VeryHigh
    }
    public static class SkillLevelHelper
    {
        /// <summary>
        /// Tries to parse the string <paramref name="s"/> to a <paramref name="skillLevel"/> enum.
        /// </summary>
        /// <param name="s">The string to convert to <paramref name="skillLevel"/>.</param>
        /// <param name="skillLevel">The SkillLevel enum equal by description to <paramref name="s"/> by description, if the conversion was successful.</param>
        /// <returns>A boolean representing whether the conversion of <paramref name="s"/> was successful or not.</returns>
        public static bool ParseEnum(string s, out SkillLevel? skillLevel)
        {
            skillLevel = null;
            foreach (var item in Enum.GetValues(typeof(SkillLevel)))
            {
                if (item == null) continue;
                var skillLevelItem = (SkillLevel)item;

                if (skillLevelItem.GetDescription().Equals(s, StringComparison.OrdinalIgnoreCase))
                {
                    skillLevel = skillLevelItem;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Gets the default Speed for the specified <paramref name="species"/>. In case of an Enemy, this method returns Average.
        /// </summary>
        /// <param name="species">The species that determines the default speed.</param>
        /// <returns>The default speed based on the given <paramref name="species"/> in SkillLevel.</returns>
        public static SkillLevel GetDefaultStrength(Species species) => species switch
        {
            Species.Stonekin => SkillLevel.VeryHigh,
            Species.Swiftlings => SkillLevel.Low,
            Species.Vitaleons => SkillLevel.High,
            Species.Shadowsneak => SkillLevel.Average,
            Species.Maguskin => SkillLevel.High,
            Species.Bruteforce => SkillLevel.VeryHigh,
            Species.Quicksylph => SkillLevel.Low,
            Species.Mysticore => SkillLevel.Low,
            Species.Ironheart => SkillLevel.High,
            Species.Fleetspell => SkillLevel.Low,
            _ => SkillLevel.Average,
        };
        /// <summary>
        /// Gets the default Speed for the specified <paramref name="species"/>.
        /// </summary>
        /// <param name="species">The species that determines the default speed.</param> In case of an Enemy, this method returns Average.
        /// <returns>The default speed based on the given <paramref name="species"/> in SkillLevel.</returns>
        public static SkillLevel GetDefaultDexterity(Species species) => species switch
        {
            Species.Stonekin => SkillLevel.Low,
            Species.Swiftlings => SkillLevel.High,
            Species.Vitaleons => SkillLevel.Low,
            Species.Shadowsneak => SkillLevel.VeryHigh,
            Species.Maguskin => SkillLevel.VeryLow,
            Species.Bruteforce => SkillLevel.Low,
            Species.Quicksylph => SkillLevel.High,
            Species.Mysticore => SkillLevel.Average,
            Species.Ironheart => SkillLevel.Average,
            Species.Fleetspell => SkillLevel.VeryLow,
            _ => SkillLevel.Average,
        };
        /// <summary>
        /// Gets the default Speed for the specified <paramref name="species"/>.
        /// </summary>
        /// <param name="species">The species that determines the default speed.</param> In case of an Enemy, this method returns Average.
        /// <returns>The default speed based on the given <paramref name="species"/> in SkillLevel.</returns>
        public static SkillLevel GetDefaultVitality(Species species) => species switch
        {
            Species.Stonekin => SkillLevel.High,
            Species.Swiftlings => SkillLevel.Average,
            Species.Vitaleons => SkillLevel.VeryHigh,
            Species.Shadowsneak => SkillLevel.VeryLow,
            Species.Maguskin => SkillLevel.Average,
            Species.Bruteforce => SkillLevel.High,
            Species.Quicksylph => SkillLevel.Average,
            Species.Mysticore => SkillLevel.High,
            Species.Ironheart => SkillLevel.VeryHigh,
            Species.Fleetspell => SkillLevel.Average,
            _ => SkillLevel.Average,
        };
        /// <summary>
        /// Gets the default Speed for the specified <paramref name="species"/>.
        /// </summary>
        /// <param name="species">The species that determines the default speed.</param> In case of an Enemy, this method returns Average.
        /// <returns>The default speed based on the given <paramref name="species"/> in SkillLevel.</returns>
        public static SkillLevel GetDefaultMagic(Species species) => species switch
        {
            Species.Stonekin => SkillLevel.VeryLow,
            Species.Swiftlings => SkillLevel.VeryLow,
            Species.Vitaleons => SkillLevel.Average,
            Species.Shadowsneak => SkillLevel.Low,
            Species.Maguskin => SkillLevel.VeryHigh,
            Species.Bruteforce => SkillLevel.VeryLow,
            Species.Quicksylph => SkillLevel.VeryLow,
            Species.Mysticore => SkillLevel.VeryHigh,
            Species.Ironheart => SkillLevel.VeryLow,
            Species.Fleetspell => SkillLevel.High,
            _ => SkillLevel.Average,
        };
        /// <summary>
        /// Gets the default Speed for the specified <paramref name="species"/>.
        /// </summary>
        /// <param name="species">The species that determines the default speed.</param> In case of an Enemy, this method returns Average.
        /// <returns>The default speed based on the given <paramref name="species"/> in SkillLevel.</returns>
        public static SkillLevel GetDefaultSpeed(Species species) => species switch
        {
            Species.Stonekin => SkillLevel.Average,
            Species.Swiftlings => SkillLevel.VeryHigh,
            Species.Vitaleons => SkillLevel.VeryLow,
            Species.Shadowsneak => SkillLevel.High,
            Species.Maguskin => SkillLevel.Low,
            Species.Bruteforce => SkillLevel.Average,
            Species.Quicksylph => SkillLevel.VeryHigh,
            Species.Mysticore => SkillLevel.VeryLow,
            Species.Ironheart => SkillLevel.Low,
            Species.Fleetspell => SkillLevel.VeryHigh,
            _ => SkillLevel.Average,
        };
    }
}
