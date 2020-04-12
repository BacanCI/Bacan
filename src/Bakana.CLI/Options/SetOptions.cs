using CommandLine;

namespace Bakana.Options
{
    [Verb("set", Hidden = true, HelpText = "Sets Bakana settings")]
    public class SetOptions
    {
        [Value(0, MetaName = "Settings key",
        HelpText = "Settings key to be updated",
        Required = true)]
        public string Key { get; set; }
        
        [Value(1, MetaName = "Settings value",
            HelpText = "Value to be updated",
            Required = true)]
        public string Value { get; set; }
    }
}