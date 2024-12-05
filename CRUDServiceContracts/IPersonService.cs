using CRUDServiceContracts.DTO;

namespace CRUDServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Adds a Person object to the list of Person objects.
        /// </summary>
        /// <param name="personAddRequest">Person object to add.</param>
        /// <returns>The Person object after adding it. (Includes newly generated Person ID.)</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Gets all persons as a list.
        /// </summary>
        /// <returns>All persons as a list.</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Gets a Person object based on given PersonID.
        /// </summary>
        /// <param name="personID">PersonID to search</param>
        /// <returns>Matching person as PersonResponse object.</returns>
        PersonResponse? GetPersonByPersonID(Guid? personID);
    }
}
