using System.Collections.Generic;

using Sample.Data;

namespace Sample.Json {
	public class PersonContractResolver2 : BasicContractResolver {

		public override Dictionary<string, string> PropertyMapping => new Dictionary<string, string> {
			{ nameof(Person.Name), "employee_name" },
			{ nameof(Person.LastName), "employee_last_name" },
			// by default Json will serialize birthday, as name comparison is case insensitive
			//{ nameof(Person.Birthday), "birthday" }
		};

		/// <summary>
		/// If we do not set to false, then Birthday will be skipped
		/// </summary>
		public override bool IgnoreNotMapped => false;
	}
}
