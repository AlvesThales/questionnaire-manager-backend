
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestionnaireManager.Infrastructure.Utils;

[TestClass]
public class ResultTests
{
    [TestMethod]
    public void Fail_ShouldReturnFailureResultWithErrorMessage()
    {
        string errorMessage = "Something went wrong";

        Result result = Result.Fail(errorMessage);

        Assert.IsFalse(result.Success);
        Assert.AreEqual(errorMessage, result.ErrorMessage);
    }
    
    [TestMethod]
    public void Fail_Generic_ShouldReturnFailureResultWithErrorMessageAndDefaultGenericValue()
    {
        string errorMessage = "Something went wrong";

        Result<int> result = Result.Fail<int>(errorMessage);

        Assert.IsFalse(result.Success);
        Assert.AreEqual(errorMessage, result.ErrorMessage);
        Assert.AreEqual(default(int), result.Value);
    }
    
    [TestMethod]
    public void Ok_ShouldReturnSuccessResultWithEmptyErrorMessage()
    {
        Result result = Result.Ok();

        Assert.IsTrue(result.Success);
        Assert.AreEqual(string.Empty, result.ErrorMessage);
    }
    
    [TestMethod]
    public void Ok_Generic_ShouldReturnSuccessResultWithValueAndEmptyErrorMessage()
    {
        int value = 42;

        Result<int> result = Result.Ok(value);

        Assert.IsTrue(result.Success);
        Assert.AreEqual(string.Empty, result.ErrorMessage);
        Assert.AreEqual(value, result.Value);
    }
    
    [TestMethod]
    public void Combine_ShouldReturnSuccessResult_WhenAllResultsAreSuccessful()
    {
        Result result1 = Result.Ok();
        Result result2 = Result.Ok();

        Result combinedResult = Result.Combine(result1, result2);

        Assert.IsTrue(combinedResult.Success);
        Assert.AreEqual(string.Empty, combinedResult.ErrorMessage);
    }
    
    [TestMethod]
    public void Combine_ShouldReturnFirstFailureResult_WhenAnyResultFails()
    {
        Result result1 = Result.Ok();
        Result result2 = Result.Fail("Error occurred");

        Result combinedResult = Result.Combine(result1, result2);

        Assert.IsFalse(combinedResult.Success);
        Assert.AreEqual("Error occurred", combinedResult.ErrorMessage);
    }
}