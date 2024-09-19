namespace TransitiveDependencyKata;

public class Tests
{

    [Test]
    public void Given_EmptyDependencies_Returns_Empty_Output()
    {
        var testData = new List<string>();
        Assert.That(TransitiveDependencyResolver.ResolveTransitives(testData), Is.EqualTo(new List<string>()));
    }
    
    [Test]
    public void Given_A_Single_Dependency_Returns_Empty_Transitives()
    {
        var testData = new List<string> {"A"};
        Assert.That(TransitiveDependencyResolver.ResolveTransitives(testData), Is.EqualTo(new List<string> {"A"}));
    }
    
    [TestCase(new[] {"AB", "B"}, new[] {"A B", "B"})]
    [TestCase(new[] {"AB"}, new[] {"A B"})]
    public void Given_A_Simple_Chain_Of_Dependencies_Returns_Single_Transitive(string[] testData, string[] expectedOutcome)
    {
        Assert.That(TransitiveDependencyResolver.ResolveTransitives(testData.ToList()), Is.EqualTo(expectedOutcome.ToList()));
    }
    
    [Test]
    [TestCase(new[] {"DAF", "FH"}, new[] {"D AFH", "F H"})]
    [TestCase(new[] {"FH", "DAF"}, new[] {"D AFH", "F H"})]
    public void Given_A_NonTrivial_Chain_Of_Dependencies_Returns_All_Transitives_In_Alphabetical_Order(string[] testData, string[] expectedOutcome)
    {
        Assert.That(TransitiveDependencyResolver.ResolveTransitives(testData.ToList()), Is.EqualTo(expectedOutcome.ToList()));
    }
    
    [Test]
    public void Part1_Complete()
    {
        var testData = new List<string>
        {
            "ABC",
            "BCE",
            "CG",
            "DAF",
            "EF",
            "FH"
        };
        
        var expectedOutcome = new List<string>
        {
            "A BCEFGH",
            "B CEFGH",
            "C G",
            "D ABCEFGH",
            "E FH",
            "F H"
        };
        
        Assert.That(TransitiveDependencyResolver.ResolveTransitives(testData), Is.EqualTo(expectedOutcome));
    }
    
    [Test]
    public void Given_Simplest_Possible_Circular_Dependency_Resolver_Still_Throws()
    {
        var testData = new List<string>
        {
            "AB",
            "BA"
        };

        var actualException = Assert.Throws<OverflowException>(() => TransitiveDependencyResolver.ResolveTransitives(testData));
        
        Assert.That(actualException.Message, Is.EqualTo("Circular dependency on `A` detected"));
    }
    
    [Test]
    public void Part2_Complete()
    {
        var testData = new List<string>
        {
            "AB",
            "BC",
            "CA"
        };

        var actualException = Assert.Throws<OverflowException>(() => TransitiveDependencyResolver.ResolveTransitives(testData));
        
        Assert.That(actualException.Message, Is.EqualTo("Circular dependency on `A` detected"));
    }
    
    [Test]
    public void Given_Hidden_Circular_Dependency_Resolver_Still_Throws()
    {
        var testData = new List<string>
        {
            "AB",
            "BC",
            "CB"
        };

        var actualException = Assert.Throws<OverflowException>(() => TransitiveDependencyResolver.ResolveTransitives(testData));
        
        Assert.That(actualException.Message, Is.EqualTo("Circular dependency on `B` detected"));
    }
}
