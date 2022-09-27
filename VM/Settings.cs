namespace CookPC.VM {
    public class Settings {
        public string Version = "0.0.0";
        // Dev, beta, release
        public string VersionType = "Release";
        public string Architecture = "leg16";
        public string ArchitectureVersion = "0";
        public int DefaultInstructionsPerFrame = 750;
        public int VariableLimit = 10240;
    }
}