﻿using Microsoft.EntityFrameworkCore;
using QuestionnaireManager.Application.Commands;
using QuestionnaireManager.Application.Commands.CreateQuestion;
using QuestionnaireManager.Application.Commands.CreateQuestionnaire;
using QuestionnaireManager.Application.Commands.CreateRootQuestion;
using QuestionnaireManager.Application.Commands.DeleteQuestion;
using QuestionnaireManager.Application.Commands.DeleteQuestionnaire;
using QuestionnaireManager.Application.Commands.UpdateQuestion;
using QuestionnaireManager.Application.Commands.UpdateQuestionnaire;
using QuestionnaireManager.Application.Queries;
using QuestionnaireManager.Application.Queries.GetQuestionById;
using QuestionnaireManager.Application.Queries.GetQuestionnaireById;
using QuestionnaireManager.Data;
using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Rest.Controllers.Utils;

namespace QuestionnaireManager.Rest.Startup;

public static class Registry
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMediator, Mediator>();
        
        services.AddScoped<ICommandHandler<CreateQuestionnaireCommand>, CreateQuestionnaireHandler>();
        services.AddScoped<ICommandHandler<UpdateQuestionnaireCommand>, UpdateQuestionnaireHandler>();
        services.AddScoped<ICommandHandler<DeleteQuestionnaireCommand>, DeleteQuestionnaireHandler>();
        
        services.AddScoped<ICommandHandler<CreateQuestionCommand>, CreateQuestionHandler>();
        services.AddScoped<ICommandHandler<CreateRootQuestionCommand>, CreateRootQuestionHandler>();
        services.AddScoped<ICommandHandler<UpdateQuestionCommand>, UpdateQuestionHandler>();
        services.AddScoped<ICommandHandler<DeleteQuestionCommand>, DeleteQuestionHandler>();

        services.AddScoped<IQueryHandlerAsync<GetQuestionnaireByIdQuery, Questionnaire>, GetQuestionnaireByIdHandler>();
        
        services.AddScoped<IQueryHandlerAsync<GetQuestionByIdQuery, Question>, GetQuestionByIdHandler>();
        
        services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();

        
        services.AddDbContext<QuestionnaireManagerContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("QuestionnaireDatabase"))
        );
    }
}