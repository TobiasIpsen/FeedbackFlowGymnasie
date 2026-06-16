using feedbackFlowAPI.DTOs;
using System;

namespace feedbackFlowAPI.Services.Interfaces
{
	public interface IQuestionAnswerService
	{
		public Task<QuestionAnswerDTO> SubmitAnswer(QuestionAnswerDTO dto);
    }
}
