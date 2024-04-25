using eXChanger.POC.Api.Models.Foundations.Persons.Exceptions;
using eXChanger.POC.Models.Foundations.Persons;
using FluentAssertions.Execution;
using System;

namespace eXChanger.POC.Services.Foundations.Persons
{
	public partial class PersonService
	{
		private void ValidatePersonId(Guid id)
		{
			Validate((Rule: IsInvalid(id), Parameter: nameof(Person.Id)));
		}
		private void ValidatePersonOnAdd(Person person)
		{
			ValidationPersonNotNull(person);

			Validate(
			  (Rule: IsInvalid(person.Id), Parameter: nameof(person.Id)),
			  (Rule: IsInvalid(person.Name), Parameter: nameof(person.Name)),
			  (Rule: IsInvalid(person.Age), Parameter: nameof(person.Age)));
		}

		private void ValidatePersonOnModify(Person person)
		{
			ValidationPersonNotNull(person);

			Validate(
			  (Rule: IsInvalid(person.Id), Parameter: nameof(person.Id)),
			  (Rule: IsInvalid(person.Name), Parameter: nameof(person.Name)),
			  (Rule: IsInvalid(person.Age), Parameter: nameof(person.Age)));
		}
		private void ValidationPersonNotNull(Person Person)
		{
			if (Person is null)
			{
				throw new NullPersonException();
			}
		}
		private static dynamic IsInvalid(Guid id) => new
		{
			Condition = id == Guid.Empty,
			Message = "Id is required"
		};

		private static dynamic IsInvalid(string text) => new
		{
			Condition = string.IsNullOrWhiteSpace(text),
			Message = "Text is invalid"
		};

		private static dynamic IsInvalid(int age) => new
		{
			Condition = age == 0,
			Message = "Age is invalid"
		};

		private static void Validate(params (dynamic Rule, string Parameter)[] validations)
		{
			var invalidPersonException = new InvalidPersonException();

			foreach ((dynamic rule, string parameter) in validations)
			{
				if (rule.Condition)
				{
					invalidPersonException.UpsertDataList(parameter, rule.Message);
				}
			}

			invalidPersonException.ThrowIfContainsErrors();
		}
	}
}
