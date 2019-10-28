using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sample.Data;
using Sample.Json;
using System;

namespace Tests {

	[TestClass]
	public class PersonTests {

		public readonly string json = @"{
			'employee_name': 'Bob',
			'employee_last_name': 'Smith',
			'birthday': '1999-01-01T00:00:01'
		}";


		[TestMethod]
		public void ReadJson() {
			Read(json, new PersonContractResolver());
			Read(json, new PersonContractResolver2());
		}

		private void Read(string json, IContractResolver resolver) {
			var person = JsonConvert.DeserializeObject<Person>(json, new JsonSerializerSettings {
				ContractResolver = resolver
			});
			Assert.AreEqual(person.Name, "Bob");
			Assert.AreEqual(person.LastName, "Smith");
			Assert.AreEqual(person.Birthday, new DateTime(1999, 1, 1, 0, 0, 1));
		}

		[TestMethod]
		public void WriteJson() {
			Write(new PersonContractResolver());
			Write(new PersonContractResolver2());
		}

		private void Write(IContractResolver resolver) { 
			Person person = new Person("Boby", "Smithy", new DateTime(1999, 1, 1, 0, 0, 1));
			// serialize
			var json = JsonConvert.SerializeObject(person, new JsonSerializerSettings {
				ContractResolver = resolver
			});
			// then deserialize
			var person2 = JsonConvert.DeserializeObject<Person>(json, new JsonSerializerSettings {
				ContractResolver = resolver
			});
			// assert
			Assert.AreEqual(person.Name, person2.Name);
			Assert.AreEqual(person.LastName, person2.LastName);
			Assert.AreEqual(person.Birthday, person2.Birthday);
		}
	}
}
