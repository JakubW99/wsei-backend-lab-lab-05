using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using WebAPI.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private IQuizUserService _service;

        public QuizController(IQuizUserService service) 
        {
            _service = service;
        }

        // GET: api/<QuizController>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<QuizDto>> FindById(int id)
        {
            QuizDto quizDto = new QuizDto();
            var quiz = _service.FindQuizById(id);

            if (quiz != null)
            {
                quizDto.Id = quiz.Id;
                quizDto.Title = quiz.Title;
                quizDto.Items = quiz.Items;
                return Ok(quizDto);
            }
            else
                return NotFound();
        }
        // GET api/<QuizController>/5
        [HttpGet]
        public IEnumerable<QuizDto> FindAll()
        {
            var quizzes = _service.FindAllQuizzes()
                .Select(q => new QuizDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    Items = q.Items
                });
            return quizzes;

        }
        [HttpPost]
        [Route("{quizId}/items/{itemId}")]
        public void SaveAnswer([FromBody] QuizItemAnswerDto dto, [FromRoute] int quizId, [FromRoute] int quizItemId)
        {
            _service.SaveUserAnswerForQuiz(quizId, dto.UserId, quizItemId, dto.Answer);
           
        }

        [HttpGet]
        [Route("CorrectAnswers/{quizId}/{userId}")]
        public int CountCorrectUserAnswers([FromRoute] int userId, [FromRoute] int quizId)
        {
            return _service.CountCorrectAnswersForQuizFilledByUser(quizId,userId);
        }
    }





}
