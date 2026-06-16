using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace feedbackFlowAPI.Services.Implementations;

public class QuestionAnswerService : IQuestionAnswerService
{
    private FbfDbContext _context;
    private readonly IStorageService _storageService;

    public QuestionAnswerService(FbfDbContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task<QuestionAnswerDTO> SubmitAnswer(QuestionAnswerDTO dto)
    {
        string? fileUrl = null;

        // Upload fil hvis den findes
        if (dto.File != null)
        {
            fileUrl = await _storageService.UploadFileAsync(dto.File, "question-answers");
        }

        var questionAnswer = new QuestionAnswer
        {
            Name = dto.Name,
            Url = fileUrl, // brug uploadet fil
            DeletedAt = dto.DeletedAt,
            QuestionId = dto.QuestionId,
            Visibility = dto.visibility
        };

        var entry = await _context.QuestionAnswers.AddAsync(questionAnswer);
        await _context.SaveChangesAsync();

        return new QuestionAnswerDTO
        {
            Name = entry.Entity.Name,
            Url = entry.Entity.Url, // url for hvor billedet ligger i storage
            DeletedAt = entry.Entity.DeletedAt,
            QuestionId = dto.QuestionId
        };
    }
}