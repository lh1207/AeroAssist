namespace AeroAssist
{
    // The SystemConfig class is utilized in a one-to-many relationship with the Ticket class,
    // allowing each ticket to reference one specific system configuration, ultimately allowing
    // the user to autofill configurable parameters with individual tickets within the application.
    public class SystemConfig(int configId, string configName, string configValue)
    {
        public int ConfigId { get; set; } = configId;
        public string ConfigName { get; set; } = configName;
        public string ConfigValue { get; set; } = configValue;

        public void CreateConfig()
        {
            ConfigId = 0; // TODO: Generate a unique configuration ID through a database query
            ConfigName = "New Configuration";
            ConfigValue = "New Configuration Value";
        }

        public void UpdateConfig()
        {
            ConfigId = 0; // TODO: Generate a unique configuration ID through a database query
            ConfigName = "Updated Configuration";
            ConfigValue = "Updated Configuration Value";
        }

        public void DeleteConfig()
        {
            ConfigId = 0; // TODO: Generate a unique configuration ID through a database query
            ConfigName = "Deleted Configuration";
            ConfigValue = "Deleted Configuration Value";
        }
    }
}
