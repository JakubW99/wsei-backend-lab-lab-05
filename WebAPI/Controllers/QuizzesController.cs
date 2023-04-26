using ApplicationCore.Interfaces.QuizUserService;
using ApplicationCore.Models;
using Infrastructure.MongoDB.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizUserServiceMongoDB mongoDB;
       public QuizzesController(QuizUserServiceMongoDB _mongoDB)
        {
           mongoDB = _mongoDB;
        }
        [HttpGet]
        public List<Quiz> Get()
        {
            return mongoDB.FindAllQuizzes();
        }

        // GET api/<QuizzesController>/5
        [HttpGet("{id}")]
        public Quiz Get(int id)
        {
            return mongoDB.FindQuizById(id);
        }

      
    }
}
