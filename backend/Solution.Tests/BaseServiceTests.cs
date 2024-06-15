namespace Solution.Tests;

public class BaseServiceTests : IAsyncLifetime
{
    protected readonly TestDatabaseBuilder testDatabaseBuilder;
    protected readonly ApplicationDbContext dbContext;

    protected const int NUMBER_OF_PREDEFINIED_HEROS = 3;

    public BaseServiceTests()
    {

        this.testDatabaseBuilder = new TestDatabaseBuilder();
        this.dbContext = testDatabaseBuilder.CreateDbContext();
    }

    public async Task InitializeAsync()
    {
        await testDatabaseBuilder.SetupTestDatabaseWithDefaultsAsync();
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }

    public async Task DisposeAsync()
    {
        await dbContext.DisposeAsync();
    }
}
