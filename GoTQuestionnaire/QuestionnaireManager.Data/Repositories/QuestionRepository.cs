using Microsoft.EntityFrameworkCore;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Data.Repositories;

public interface IQuestionRepository
{
    Task<Question?> GetByIdAsync(int id);
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

    public async Task<Question?> GetByIdAsync(int id)
    {
        return await _context.Questions.FindAsync(id);
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

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();

        return Result.Ok(); 
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}