using System;

public class School
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Classroom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SchoolId { get; set; }
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ClassroomId { get; set; }
}

