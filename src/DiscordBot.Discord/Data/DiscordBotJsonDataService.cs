﻿using DiscordBot.Discord.Data.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace DiscordBot.Discord.Data
{
    public class DiscordBotJsonDataService : IDiscordBotJsonDataService
    {
        //TODO: Refactor

        /// <summary>
        /// Retreives data from a json file.
        /// </summary>
        /// <typeparam name="T">The Type you want to retrieve.</typeparam>
        /// <param name="path">The path to the file you want to load.</param>
        /// <returns></returns>
        public Task<T> Retreive<T>(string path)
        {
            EnsureExists(path);
            var rawData = GetRawData(path);
            return Task.FromResult(JsonConvert.DeserializeObject<T>(rawData)); // eewww
        }

        /// <summary>
        /// Saves JSON data into a specified path.
        /// </summary>
        /// <param name="obj">The Object you wish to serialize and save.</param>
        /// <param name="path">Where you want to save to.</param>
        /// <returns></returns>
        public async Task Save(object obj, string path)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            EnsureExists(path);
            await File.WriteAllTextAsync(path, json);
        }

        /// <summary>
        /// Checks if the specified path exists.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns></returns>
        public bool FileExists(string path)
            => File.Exists(path);

        private void EnsureExists(string path)
        {
            if (!FileExists(path))
                CreateFile(path);
        }

        private void CreateFile(string path)
        {
            if (!Directory.Exists(Global.ResourcesFolder))
                Directory.CreateDirectory(Global.ResourcesFolder);
            if (FileExists(path))
                File.Create(path);
        }

        private string GetRawData(string path)
            => File.ReadAllText(path);
    }
}