using System;
using System.Collections.Generic;
using System.Text;
using Sample.Data;

namespace Sample.Json {
	public class PersonContractResolver : BasicContractResolver {

		/// <summary>
		/// Maps Person properties to json property names
		/// </summary>
		public override Dictionary<string, string> PropertyMapping => new Dictionary<string, string> {
			// we use nameof(), so we would not break mapping with refactoring
			{ nameof(Person.Name), "employee_name" },
			{ nameof(Person.LastName), "employee_last_name" },
			{ nameof(Person.Birthday), "birthday" }
		};

	}
}
