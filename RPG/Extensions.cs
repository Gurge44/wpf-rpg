using System.ComponentModel;
using System.Reflection;

namespace RPG
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T value) where T : struct
        {
            Type type = value.GetType();
            if (!type.IsEnum) return string.Empty;

            MemberInfo[] memberInfo = type.GetMember(value.ToString() ?? string.Empty);

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return value.ToString() ?? string.Empty;
        }

        public static T? RandomElement<T>(this HashSet<T> values)
        {
            if (values == null || values.Count == 0) return default;

            int index = Main.Random.Next(0, values.Count);
            T element = values.ElementAt(index);
            return element;
        }
    }
}
