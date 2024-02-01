using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Character(string name, Species species, SkillLevel strength, SkillLevel dexterity, SkillLevel vitality, SkillLevel magic, SkillLevel speed, string imageURL)
    {
        public string Name { get => name; set => name = value; }
        public Species Species { get => species; set => species = value; }
        public SkillLevel Strength { get => strength; set => strength = value; }
        public SkillLevel Dexterity { get => dexterity; set => dexterity = value; }
        public SkillLevel Vitality { get => vitality; set => vitality = value; }
        public SkillLevel Magic { get => magic; set => magic = value; }
        public SkillLevel Speed { get => speed; set => speed = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }

        public void SetAllValuesToDefault()
        {
            Strength = SkillLevelHelper.GetDefaultStrength(Species);
            Dexterity = SkillLevelHelper.GetDefaultDexterity(Species);
            Vitality = SkillLevelHelper.GetDefaultVitality(Species);
            Magic = SkillLevelHelper.GetDefaultMagic(Species);
            Speed = SkillLevelHelper.GetDefaultSpeed(Species);
        }
    }
    public static class CharacterHelper
    {
        public static Character GetDefaultCharacter()
        {
            var character = new Character(string.Empty, Species.Stonekin, SkillLevel.Average, SkillLevel.Average, SkillLevel.Average, SkillLevel.Average, SkillLevel.Average, string.Empty);
            character.SetAllValuesToDefault();
            return character;
        }
    }
}
