using System;

namespace RealEstateAgency.Shared.Services
{
    public class FastHasher : IFastHasher
    {
        public string CalculateHash(string toHash)
        {
            var hashedValue = 3074457345618258791ul;
            foreach (var t in toHash)
            {
                hashedValue += t;
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue.ToString();
        }

        public string CalculateTimeHash(string toHash)
            => CalculateHash(toHash + DateTime.Now.Ticks);

        public string CalculateHash(string key, string toHash)
            => CalculateHash(key + toHash);

        public string FileNameHash(string category, string key)
            => $"{category}_{CalculateHash(category.Trim().ToLower() + key.Trim().ToLower())}";
    }

    public interface IFastHasher
    {
        string CalculateHash(string toHash);

        string CalculateTimeHash(string toHash);

        string CalculateHash(string key, string toHash);

        string FileNameHash(string category, string key);
    }
}
