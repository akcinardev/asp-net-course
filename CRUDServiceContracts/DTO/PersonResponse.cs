﻿using CRUDEntities;

namespace CRUDServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as return type for most of PersonService methods.
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public Guid? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        // It compares the current object to another object of CountryResponse
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;
            return 
                this.PersonID == person.PersonID && 
                this.PersonName == person.PersonName &&
                this.Email == person.Email &&
                this.DateOfBirth == person.DateOfBirth &&
                this.Gender == person.Gender &&
                this.CountryID == person.CountryID &&
                this.Address == person.Address &&
                this.ReceiveNewsLetters == person.ReceiveNewsLetters;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class PersonExtensions
    {
        /// <summary>
        /// An extension method to convert an object of Person class into PersonResponse class.
        /// </summary>
        /// <param name="person">Person object to convert.</param>
        /// <returns>The converted PersonResponse object.</returns>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                CountryID = person.CountryID,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null,
            };
        }
    }
}