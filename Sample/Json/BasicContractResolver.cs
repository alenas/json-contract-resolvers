using System.Collections.Generic;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sample.Json {
	public abstract class BasicContractResolver : DefaultContractResolver {

		/// <summary>
		/// Mapping between Class Property Name and JSON Property Name
		/// </summary>
		public abstract Dictionary<string, string> PropertyMapping { get; }

		/// <summary>
		/// By default ignores properties that are not in the PropertyMapping
		/// </summary>
		public virtual bool IgnoreNotMapped { get; } = true;

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
			var property = base.CreateProperty(member, memberSerialization);
			if (PropertyMapping.TryGetValue(member.Name, out string name)) {
				property.PropertyName = name;
			} else if (IgnoreNotMapped) {
				property.Ignored = true;
			}
			// else we could throw error
			return property;
		}
	}
}
