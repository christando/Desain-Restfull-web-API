using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Http;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresensiHarianGuruController : ControllerBase
{
    private readonly BooksService _booksService;

    public PresensiHarianGuruController(BooksService booksService) =>
        _booksService = booksService;


    
    [HttpGet]
    //[Authorize]

    
    public async Task<List<Book>> Get() =>
        await _booksService.GetAsync();

/// <summary>
/// show a specific BookStore Item by using id.
/// </summary>
/// <param name="id"></param>
/// <remarks>
/// Sample request:
///
///     POST /Books
///     {
///        "id": "String",
///        "name": "Books #1",
///        "Price": 0,
///        "Category": "Category #1", 
///        "Author": "String"   
///     }
///
/// </remarks>
/// <response code="201">Returns the newly created item</response>
/// <response code="400">If the item is null</response>
/// <response code="401">requires authentication</response>
/// <response code="404">Error Not Found</response>
/// <response code="500">Internal Server Error</response>
    [HttpGet("{id:length(24)}")]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

/// <summary>
/// Post a new BookStore Item .
/// </summary>
/// <returns>A newly created BookStore Item </returns>
/// <remarks>
/// Sample request:
///
///     POST /Books
///     {
///        "id": "String",
///        "name": "Books #1",
///        "Price": 0,
///        "Category": "Category #1", 
///        "Author": "String"   
///     }
///
/// </remarks>
///
/// <response code="201">Returns the newly created item</response>
/// <response code="400">If the item is null</response>
/// <response code="401">requires authentication</response>
/// <response code="404">Error Not Found</response>
/// <response code="500">Internal Server Error</response>

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(Book newBook)
    {
        try
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            await _booksService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error retrieving data from the database");
        }
    }

/// <summary>
/// Put a specific BookStore Item by using id.
/// </summary>
/// <param name="id"></param>
/// <remarks>
/// Sample request:
///
///     POST /Books
///     {
///        "id": "String",
///        "name": "Books #1",
///        "Price": 0,
///        "Category": "Category #1", 
///        "Author": "String"   
///     }
///
/// </remarks>
/// <response code="201">Returns the newly created item</response>
/// <response code="400">If the item is null</response>
/// <response code="401">requires authentication</response>
/// <response code="404">Error Not Found</response>
/// <response code="500">Internal Server Error</response>

    [HttpPut("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _booksService.UpdateAsync(id, updatedBook);

        return NoContent();
    }


/// <summary>
/// Deletes a specific BookStore Item by using id.
/// </summary>
/// <param name="id"></param>
/// <remarks>
/// Sample request:
///
///     POST /Books
///     {
///        "id": "String",
///        "name": "Books #1",
///        "Price": 0,
///        "Category": "Category #1", 
///        "Author": "String"   
///     }
///
/// </remarks>
/// <response code="201">Returns the newly created item</response>
/// <response code="400">If the item is null</response>
/// <response code="401">requires authentication</response>
/// <response code="404">Error Not Found</response>
/// <response code="500">Internal Server Error</response>

    
    [HttpDelete("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Delete(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _booksService.RemoveAsync(id);

        return NoContent();
    }
}