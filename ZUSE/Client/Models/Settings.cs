using System;
namespace ZUSE.Client.Models
{
	public class Settings
	{
        public List<kdsSetting> list { get; set; } = new();

		public kdsSetting getSetting(settingId settingType)
		{
			return list.Where(s => s.settingType == settingType).SingleOrDefault();
		}
		public void Add(kdsSetting kdsSetting)
		{
			//if (list.Find(s => s.settingType == kdsSetting.settingType) is not null)
			//	return;
			list.Add(kdsSetting);
		}
	}
}

