using System.Collections;
using System.Collections.Generic;

namespace NetherWars.Data
{
	public class DataObject : Hashtable
	{
		private Scheme _scheme;

		public DataObject(Scheme scehme)
		{
			Scheme = scehme;
		}

		private void LoadScheme(Scheme scheme)
		{
			foreach (string key in _scheme.SchemeInfo.Keys)
			{
				if (_scheme.SchemeInfo[key] is IList)
				{
					Add(key, new List<DataObject>());
				}
				else
				{
					Add(key, null);
				}
			}
		}

		public Scheme Scheme
		{
			set
			{
				if (_scheme.SchemeType != value.SchemeType)
				{
					_scheme = value;
					LoadScheme(_scheme);
				}
			}
			get
			{
				return _scheme;
			}
		}

		public override object this[object index] {
			get {
				return base[index];
			}
			set {
				base[index] = value;
			}
		}
	
		public override void Add (object key, object value)
		{
			base.Add (key, value);
		}
	}


	public class Scheme
	{
		public eSchemeType SchemeType;

		public  Dictionary<string, object> SchemeInfo;

		public Scheme(eSchemeType schemeType)
		{
			SchemeType = schemeType;

			SchemeInfo = SchemeLoader.GetScheme(schemeType);
		}
	}


	public enum eObjectType
	{
		Int,
		Float,
		String,
		Power,
		Trigger,
		Effect,
		Target,
		Count,
		Var,
	}

	public enum eSchemeType
	{
		Power,
		Trigger,
		Effect,
		Target,
		Count,
		Var,

	}

	public static class SchemeLoader
	{
		public static Dictionary<eSchemeType, Dictionary<string, object>> Schemes;

		public static void LoadScehems()
		{
			Dictionary<string, eObjectType> newScheme = new Dictionary<string, eObjectType>();



		}

		public static Dictionary<string, object> GetScheme(eSchemeType schemeType)
		{
			if (Schemes.ContainsKey(schemeType))
			{
				return Schemes[schemeType];
			}
			return null;
		}
	}
}

