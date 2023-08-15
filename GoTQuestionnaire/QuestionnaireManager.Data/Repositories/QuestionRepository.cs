using Microsoft.EntityFrameworkCore;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Data.Repositories;

public interface IQuestionRepository
{
    Task<Question?> GetByIdAsync(int questionnaireId, int questionId);
    Task<Result> UpdateAsync(int id, string description);
    Task<Result> DeleteAsync(int id);
    Task AddAsync(Question question);
    Task SaveChangesAsync();
}

public class QuestionRepository : IQuestionRepository
{
    private readonly QuestionnaireManagerContext _context;

    public QuestionRepository(QuestionnaireManagerContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
    }

    public async Task<Question?> GetByIdAsync(int questionnaireId, int questionId)
    {
        return await _context.Questions
            .FirstOrDefaultAsync(x => x.QuestionnaireId == questionnaireId && x.Id == questionId);
    }

    public async Task<Result> UpdateAsync(int id, string description)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null)
            return Result.Fail("Question not found");

        question.Description = description;

        _context.Entry(question).State = EntityState.Modified;
        _context.Update(question);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null)
            return Result.Fail("Question not found");

        DeleteQuestionWithRelatedAnswers(question);
        
        await _context.SaveChangesAsync();

        return Result.Ok(); 
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    private void DeleteQuestionWithRelatedAnswers(Question question)
    {
        if (question.Answers != null)
        {
            foreach (var childQuestion in question.Answers.Select(a => a.ChildQuestion).ToList())
            {
                if (childQuestion != null) DeleteQuestionWithRelatedAnswers(childQuestion);
            }
            
            foreach (var answer in question.Answers.ToList())
            {
                _context.Answers.Remove(answer);
            }
        }

        _context.Questions.Remove(question);
    }
}