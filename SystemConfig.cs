namespace AeroAssist
{
    /// <summary>
    /// Represents a system configuration that can be associated with tickets.
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// Gets or sets the configuration ID.
        /// </summary>
        public int ConfigId { get; set; }

        /// <summary>
        /// Gets or sets the configuration name.
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// Gets or sets the configuration value.
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemConfig"/> class.
        /// </summary>
        /// <param name="configId">The configuration ID.</param>
        /// <param name="configName">The configuration name.</param>
        /// <param name="configValue">The configuration value.</param>
        public SystemConfig(int configId, string configName, string configValue)
        {
            ConfigId = configId;
            ConfigName = configName;
            ConfigValue = configValue;
        }

        /// <summary>
        /// Creates a new configuration with default values.
        /// </summary>
        public void CreateConfig()
        {
            ConfigId = 0; // TODO: Generate a unique configuration ID through a database query
            ConfigName = "New Configuration";
            ConfigValue = "New Configuration Value";
        }

        /// <summary>
        /// Updates the configuration with new values.
        /// </summary>
        public void UpdateConfig()
        {
            ConfigId = 0; // TODO: Generate a unique configuration ID through a database query
            ConfigName = "Updated Configuration";
            ConfigValue = "Updated Configuration Value";
        }

        /// <summary>
        /// Deletes the configuration and sets its values to indicate deletion.
        /// </summary>
        public void DeleteConfig()
        {
            ConfigId = 0; // TODO: Generate a unique configuration ID through a database query
            ConfigName = "Deleted Configuration";
            ConfigValue = "Deleted Configuration Value";
        }
    }
}