using Microsoft.EntityFrameworkCore;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Data.Repositories;


public interface IQuestionnaireRepository
{
    Task<Questionnaire?> GetByIdAsync(int id);
    Task<List<Questionnaire>> GetAllAsync();
    Task AddAsync(Questionnaire questionnaire);
    Task SaveChangesAsync();
    Task<Result> UpdateAsync(int id, string name, int maxQuestions, int maxAnswers);
    Task<Result> DeleteAsync(int id);

}

public class QuestionnaireRepository : IQuestionnaireRepository
{
    private readonly QuestionnaireManagerContext _context;

    public QuestionnaireRepository(QuestionnaireManagerContext context)
    {
        _context = context;
    }

    public async Task<Questionnaire?> GetByIdAsync(int id)
    {
        return await _context.Questionnaires.FindAsync(id);
    }
    
    public async Task<List<Questionnaire>> GetAllAsync()
    {
        return await _context.Questionnaires.ToListAsync();
    }

    public async Task AddAsync(Questionnaire questionnaire)
    {
        await _context.Questionnaires.AddAsync(questionnaire);
    }
    
    public async Task<Result> UpdateAsync(int id, string name, int maxQuestions, int maxAnswers)
    {
        var questionnaire = await _context.Questionnaires.FindAsync(id);
        if (questionnaire == null)
            return Result.Fail("Questionnaire not found");

        questionnaire.Name = name;
        questionnaire.MaxQuestions = maxQuestions;
        questionnaire.MaxAnswers = maxAnswers;

        _context.Entry(questionnaire).State = EntityState.Modified;
        _context.Update(questionnaire);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }
    
    public async Task<Result> DeleteAsync(int id)
    {
        var questionnaire = _context.Questionnaires
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .SingleOrDefault(q => q.Id == id);

        if (questionnaire != null)
        {
            foreach (var question in questionnaire.Questions)
            {
                DeleteQuestionWithRelatedAnswers(question);
            }
            
            _context.Questionnaires.Remove(questionnaire);

            await _context.SaveChangesAsync();
        }

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