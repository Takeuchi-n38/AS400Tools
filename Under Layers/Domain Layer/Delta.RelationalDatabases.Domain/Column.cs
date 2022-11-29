namespace Delta.RelationalDatabases
{
    public class Column
    {
        public readonly string Name;
        public Column(string aName)
        {
            Name = aName;
        }

        public Column clone()
        {
            return new Column(Name);
        }
    }
}
