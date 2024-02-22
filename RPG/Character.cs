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

        public bool IsEnemy => Species == Species.Enemy;

        public override string ToString() => string.Join('*', [Name, $"{(int)Species}", $"{(int)Strength}", $"{(int)Dexterity}", $"{(int)Vitality}", $"{(int)Magic}", $"{(int)Speed}", ImageURL]);

        public override bool Equals(object? obj)
        {
            if (obj is not Character x) return base.Equals(obj);
            if (obj is null) return false;
            return Species == x.Species && Strength == x.Strength && Dexterity == x.Dexterity && Vitality == x.Vitality && Magic == x.Magic && Speed == x.Speed;
        }

        public override int GetHashCode() => Name.ToCharArray().Sum(x => x) + base.GetHashCode();
    }
    public static class CharacterHelper
    {
        public static Character GetDefaultCharacter()
        {
            var character = new Character(string.Empty, Species.Human, SkillLevel.Average, SkillLevel.Average, SkillLevel.Average, SkillLevel.Average, SkillLevel.Average, string.Empty);
            character.SetAllValuesToDefault();
            return character;
        }
    }
}
