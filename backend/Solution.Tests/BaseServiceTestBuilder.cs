namespace Solution.Tests;

public abstract class BaseServiceTestBuilder
{
    public readonly HttpContextAccessor HttpContextAccessor;

    protected BaseServiceTestBuilder()
    {
        HttpContextAccessor ??= new HttpContextAccessor();
        HttpContextAccessor.HttpContext ??= new DefaultHttpContext();
    }
}
