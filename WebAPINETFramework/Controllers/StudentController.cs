using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPINETFramework.Models;

namespace WebAPINETFramework.Controllers
{
    public class StudentController : ApiController
    {
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Paolo Guerero", Grade = "A" },
            new Student { Id = 2, Name = "Gianlula Lapadula", Grade = "B"},
            new Student { Id = 3, Name = "Pedro Gallese", Grade = "C"}
        };

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(students);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var student = students.Where(x => x.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Invalid data");

            student.Id = students.Count + 1;
            students.Add(student);

            return Created(new Uri(Request.RequestUri + "/" + student.Id), student);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Student student)
        {
            var existingStudent = students.FirstOrDefault(x => x.Id == id);
            if (existingStudent == null)
                return NotFound();

            existingStudent.Name = student.Name;
            existingStudent.Grade = student.Grade;

            return Ok(existingStudent);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingStudent = students.FirstOrDefault(x => x.Id == id);
            if (existingStudent == null)
                return NotFound();

            students.Remove(existingStudent);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
