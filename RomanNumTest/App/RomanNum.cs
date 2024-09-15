using System.Text;

namespace App {
    public record RomanNum(int value) {
        public RomanNum(string input) : this(RomanNumFactory.ParseAsInt(input)) { }
        public override string? ToString() {
            if (value == 0) return "N";
            Dictionary<int, String> parts = new() {
                { 1000, "M" },
                { 900, "CM" },
                { 500, "D" },
                { 400, "CD" },
                { 100, "C" },
                { 90, "XC" },
                { 50, "L" },
                { 40, "XL" },
                { 10, "X" },
                { 9, "IX" },
                { 5, "V" },
                { 4, "IV" },
                { 1, "I" },
            };
            int val = value;
            StringBuilder sb = new();
            foreach (var part in parts)  {
                while (val >= part.Key) {
                    val -= part.Key;
                    sb.Append(part.Value);
                }
            }
            return sb.ToString();
        }
        public Int16 ToShort() => (short)value;
        public UInt16 ToUnsignedShort() => (ushort)value;
        public Int32 ToInt() => value;
        public UInt32 ToUnsignedInt() => (uint)value;
        public Single ToFloat() => (float)value;
        public Double ToDouble() => (double)value;
    }
}