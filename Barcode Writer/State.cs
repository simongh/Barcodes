using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Barcodes
{
	/// <summary>
	/// Drawing state used to pass between methods
	/// </summary>
	public class State
	{
		/// <summary>
		/// Gets or sets the current left position in pixels
		/// </summary>
		public int Left
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the current top position in pixels
		/// </summary>
		public int Top
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the current canvas
		/// </summary>
		public Graphics Canvas
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the current barcode settings
		/// </summary>
		public BarcodeSettings Settings
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the module to be drawn
		/// </summary>
		public char ModuleValue
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public CodedValueCollection Codes
		{
			get;
			set;
		}

		public State(BarcodeSettings settings, int left, int top)
		{
			Left = left;
			Top = top;
			Settings = settings;
		}
	}
}
