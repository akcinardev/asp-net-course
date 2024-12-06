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

        /// <summary>
        /// Get all person objects that matches with the given search field and search string.
        /// </summary>
        /// <param name="searchBy">Search field to search.</param>
        /// <param name="searchString">Search string to search.</param>
        /// <returns>A list of Person objects that matches with the given search field and search string.</returns>
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);
    }
}
