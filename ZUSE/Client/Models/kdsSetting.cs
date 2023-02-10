using System;
using Blazored.LocalStorage;
using ZUSE.Shared.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Blazored.LocalStorage.ISyncLocalStorageService;

namespace ZUSE.Client.Models
{
    public enum settingId
    {
        posCategory,
        table,
        section
    }
    public class kdsSetting
    {
        public string name { get; set; } = string.Empty;
        public string storageKey { get; set; } = string.Empty;
        public settingId settingType { get; set; }
        public List<string> filter { get; set; } = new();
        public options fullOptions { get; set; } = new();


        public void SaveSettings(ISyncLocalStorageService localStorageService)
        {
            localStorageService.SetItem<List<string>>(storageKey, filter);
        }
        public void Select(string selectedFilter)
        {
            if (this.filter.Contains(selectedFilter))
            {
                this.filter.Remove(selectedFilter);
            }
            else
            {
                this.filter.Add(selectedFilter);
            }
        }
    }
}

