using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly DatabaseHelper _dbHelper;

    public StudentController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    [HttpGet]
    public IActionResult GetStudents()
    {
        var students = _dbHelper.GetStudents();
        return Ok(students);
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] Student student)
    {
        _dbHelper.AddStudent(student);
        _dbHelper.LogOperation("Added student");
        return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, student);
    }

    // Implement other CRUD operations for School and Classroom
}