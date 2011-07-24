using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Barcodes.Web
{
	/// <summary>
	/// Configuraton class for barcodes
	/// </summary>
	public class Config : ConfigurationSection
	{
		private const string SECTIONNAME = "barcodes";
		private const string IMGURL = "generatorUrl";

		/// <summary>
		/// Gets or sets the image generator path
		/// </summary>
		[ConfigurationProperty(IMGURL, IsRequired = true)]
		public string Url
		{
			get { return (string)this[IMGURL]; }
			set { this[IMGURL] = value; }
		}

		/// <summary>
		/// Gets an instance of this config
		/// </summary>
		public static Config Instance
		{
			get { return (Config)ConfigurationManager.GetSection(SECTIONNAME); }
		}
	}
}
