namespace Roomie.Common.ScriptingLanguage
{
    public interface IScriptCommand
    {
        string FullName { get; }
        ScriptCommandParameters Parameters { get; }
        ScriptCommandList InnerCommands { get; }
        string OriginalText { get; }
    }
}