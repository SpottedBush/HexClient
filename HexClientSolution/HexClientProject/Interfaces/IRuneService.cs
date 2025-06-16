using System;
using System.Threading.Tasks;
using HexClientProject.Models.RuneSystem;

namespace HexClientProject.Interfaces;

public interface IRuneService
{
    /// <summary>
    /// Creates a new rune page and sets it as the currently selected rune page.
    /// </summary>
    /// <remarks>
    /// This method interacts with the LCU (Online Mode) to create a new rune page.
    /// If the creation process fails, an exception is thrown.
    /// Note: This method requires a LoadRunePages() call to be made afterward.
    /// </remarks>
    /// <exception cref="Exception">
    /// Thrown if the creation of the rune page fails due to an invalid or empty server response.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the server response cannot be deserialized into the expected format.
    /// </exception>
    public void CreateRunePage();

    /// <summary>
    /// Saves the current rune page to the server using the provided page ID.
    /// </summary>
    /// <remarks>
    /// This method updates the existing rune page on the server with the chosen runes and stat modifiers.
    /// It interacts with the underlying Rune API to persist changes for the specified rune page.
    /// RuneMock implementation does nothing itself (Can't interact with the server).
    /// </remarks>
    /// <exception cref="Exception">
    /// Thrown if the update operation fails or the server returns an error.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the server response is invalid or cannot be processed.
    /// </exception>
    public void SaveRunePage(RunePageModel runePage);

    /// <summary>
    /// Sets the specified rune page as the currently selected rune page.
    /// </summary>
    /// <param name="pageId">
    /// The unique identifier of the rune page to be selected.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="pageId"/> is invalid or does not correspond to an existing rune page.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the server response is null, invalid, or cannot be processed.
    /// </exception>
    public void SelectCurrentRunePage(int pageId);

    /// <summary>
    /// Deletes a rune page identified by its ID.
    /// </summary>
    /// <remarks>
    /// This method removes the specified rune page from the user's collection.
    /// If the page deletion fails, an error or exception may be encountered
    /// depending on the implementation and state synchronization with the server.
    /// </remarks>
    /// <param name="pageId">
    /// The unique identifier of the rune page to be deleted.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the provided page ID is out of range or invalid.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the deletion process fails due to an unexpected error or server-side issue.
    /// </exception>
    public void DeleteRunePage(int pageId);

    /// <summary>
    /// Renames an existing rune page identified by its ID.
    /// </summary>
    /// <remarks>
    /// This method updates the name of the specified rune page.
    /// If the update process fails, an exception may be thrown indicating the reason.
    /// </remarks>
    /// <param name="pageId">
    /// The unique identifier of the rune page to be renamed.
    /// </param>
    /// <param name="newPageName">
    /// The new name to assign to the rune page.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided page ID is invalid or cannot be found.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the renaming operation fails due to an unexpected error or server-side issue.
    /// </exception>
    public void RenameRunePage(int pageId, string newPageName);

    /// <summary>
    /// Retrieves the current rune page inventory, meaning the maximum allowable rune pages and owned rune page count.
    /// </summary>
    /// <remarks>
    /// This method fetches the rune page inventory data through an API call. The retrieved information updates the application's state to reflect the current rune page limits and owned pages.
    /// If the API response is empty or null, an exception is thrown. Deserialization of the response is required to extract relevant values, and failure to deserialize correctly results in an error.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the API response cannot be deserialized into an expected format.
    /// </exception>
    public void GetPageInventory();

    /// <summary>
    /// Loads rune pages from a data source (LCU when in online mode, Mocked data otherwise) into the current runtime environment.
    /// </summary>
    /// <remarks>
    /// This method retrieves rune page data from a local or remote source, processes it, and initializes the related data models.
    /// The data is cleared and updated in `RuneStateManager`.
    /// </remarks>
    /// <returns>
    /// A task representing the asynchronous operation of loading rune pages.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the response cannot be deserialized into the expected format in the `RuneBuilder` implementation.
    /// </exception>
    /// <exception cref="Exception">
    /// Can be raised by underlying calls in the process of fetching or deserializing data.
    /// </exception>
    public Task LoadRunePages();
}