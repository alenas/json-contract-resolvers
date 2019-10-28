using System;

namespace Sample.Data {

    public class Person {

        public string Name { get; }

        public string LastName { get; }

        public DateTime Birthday { get; }

		/// <summary>
		/// We use the same parameter names as property names, just with different casing,
		/// so it would be easy to map json fields to both Properties and Constructor parameters.
		/// </summary>
		/// <param name="name">Name</param>
		/// <param name="lastName">LastName</param>
		/// <param name="birthday">Birthday</param>
        public Person(string name, string lastName, DateTime birthday) {
			Name = name;
			LastName = lastName;
			Birthday = birthday;
        }

    }
}
