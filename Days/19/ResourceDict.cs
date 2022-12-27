namespace Aoc2022.Days._19;

public class ResourceDict : Dictionary<ResourceType, int>, IEquatable<ResourceDict>

{
    public ResourceDict(IDictionary<ResourceType, int> dictionary) : base(dictionary)
    {
    }

    public ResourceDict():base()
    {
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            foreach (var kvp in this)
            {
                hash = hash * 23 + kvp.Key.GetHashCode();
                hash = hash * 23 + kvp.Value.GetHashCode();
            }
            return hash;
        }
    }

    public bool Equals(ResourceDict? other)
    {
        if (other == null)
        {
            return false;
        }
        return Count == other.Count && !this.Except(other).Any();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ResourceDict)obj);
    }
}