using CRUDServiceContracts.DTO;
using CRUDServiceContracts.Enums;

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

        /// <summary>
        /// Sorts all person objects by given property and given order. 
        /// </summary>
        /// <param name="allPersons">The list of persons to sort.</param>
        /// <param name="sortBy">Name of the prop which the persons should be sorted by.</param>
        /// <param name="sortOrder">Whether ascending ASC or descending DESC</param>
        /// <returns></returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);

        /// <summary>
        /// Updates the given Person object.
        /// </summary>
        /// <param name="personUpdateRequest">Person details to update.</param>
        /// <returns>The updated Person object as PersonResponse.</returns>
        PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);

        /// <summary>
        /// Deletes the given Person object.
        /// </summary>
        /// <param name="personID">PersonID of the Person you want to delete.</param>
        /// <returns>True if the delete successfull, otherwise false.</returns>
        bool DeletePerson(Guid? personID);
    }
}
