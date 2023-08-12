﻿using Microsoft.EntityFrameworkCore;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Data.Repositories;

public interface IAnswerRepository
{
    Task<Answer?> GetByIdAsync(int id);
    Task<Result> UpdateAsync(int id, string description);
    Task<Result> DeleteAsync(int id);
    Task AddAsync(Answer answer);
    Task SaveChangesAsync();
}

public class AnswerRepository : IAnswerRepository
{
    private readonly QuestionnaireManagerContext _context;

    public AnswerRepository(QuestionnaireManagerContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Answer answer)
    {
        await _context.Answers.AddAsync(answer);
    }
    
    public async Task<Answer?> GetByIdAsync(int id)
    {
        return await _context.Answers.FindAsync(id);
    }
    
    public async Task<Result> UpdateAsync(int id, string description)
    {
        var answer = await _context.Answers.FindAsync(id);
        if (answer == null)
            return Result.Fail("Answer not found");

        answer.Description = description;

        _context.Entry(answer).State = EntityState.Modified;
        _context.Update(answer);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var answer = await _context.Answers.FindAsync(id);
        if (answer == null)
            return Result.Fail("Answer not found");

        _context.Answers.Remove(answer);
        await _context.SaveChangesAsync();

        return Result.Ok(); 
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}