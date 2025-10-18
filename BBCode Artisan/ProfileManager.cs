using Newtonsoft.Json;

namespace BBCode_Artisan
{
    public class Profile
    {
        public string Name { get; set; } = "Default";
        public string FontFamily { get; set; } = "Arial";
        public int FontSize { get; set; } = 12;
        public string TextColor { get; set; } = "#000000";
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public int DefaultImageWidth { get; set; } = 0;
        public int DefaultImageHeight { get; set; } = 0;
        public bool ReplaceNewlinesWithBr { get; set; } = true;
        public string DefaultUrl { get; set; } = string.Empty;
    }

    public class ProfileManager
    {
        private readonly string _profilesPath;

        public ProfileManager()
        {
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "BBCode Artisan"
            );
            Directory.CreateDirectory(appDataPath);
            _profilesPath = Path.Combine(appDataPath, "profiles.json");
        }

        public List<Profile> LoadProfiles()
        {
            try
            {
                if (File.Exists(_profilesPath))
                {
                    var json = File.ReadAllText(_profilesPath);
                    var profiles = JsonConvert.DeserializeObject<List<Profile>>(json);
                    return profiles ?? new List<Profile> { new Profile() };
                }
            }
            catch
            {
            }

            return new List<Profile> { new Profile() };
        }

        public void SaveProfiles(List<Profile> profiles)
        {
            try
            {
                var json = JsonConvert.SerializeObject(profiles, Formatting.Indented);
                File.WriteAllText(_profilesPath, json);
            }
            catch
            {
            }
        }

        public void SaveProfile(Profile profile)
        {
            var profiles = LoadProfiles();
            var existing = profiles.FirstOrDefault(p => p.Name == profile.Name);

            if (existing != null)
            {
                profiles.Remove(existing);
            }

            profiles.Add(profile);
            SaveProfiles(profiles);
        }

        public void DeleteProfile(string profileName)
        {
            var profiles = LoadProfiles();
            profiles.RemoveAll(p => p.Name == profileName);
            SaveProfiles(profiles);
        }

        public Profile? GetProfile(string profileName)
        {
            var profiles = LoadProfiles();
            return profiles.FirstOrDefault(p => p.Name == profileName);
        }
    }
}
