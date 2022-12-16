using System.Text;

namespace Aoc2022.Days._13;

[DebuggerDisplay("{AsString}")]
public class NestedArray : List<object>
{
    public NestedArray? Parent { get; }

    public NestedArray(NestedArray? parent)
    {
        Parent = parent;
    }
    public string AsString
    {
        get
        {
            var bld = new StringBuilder();
            bld.Append("[");
            foreach (var item in this)
            {
                bld.Append(item switch
                {
                    NestedArray a => a.AsString,
                    string s => $"\"{s}\"",
                    _ => item.ToString()
                });
                if (item != this.Last())
                {
                    bld.Append(",");
                }
            }

            bld.Append("]");
            return bld.ToString();
        }
    }
}