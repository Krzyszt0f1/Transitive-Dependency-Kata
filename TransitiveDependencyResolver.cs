namespace TransitiveDependencyKata;

public static class TransitiveDependencyResolver
{
    public static List<string> ResolveTransitives(List<string> dependencies)
    {
        var unsortedDependenciesWithTransitives = dependencies
            .Select(line =>
                line.Length > 1 ?
                    line[0] + " " + FindTransitivesForDependency(line, dependencies) :
                    line).ToList();

        // Sort final lines should it not be in alphabetical order
        unsortedDependenciesWithTransitives.Sort();

        return unsortedDependenciesWithTransitives;
    }

    private static string FindTransitivesForDependency(string line, List<string> dependencies)
    {
        var unsortedTransitives =
            line[1..].Select(transitive => FindTransitivePathsForDependency([line[0]], transitive, dependencies))
                .SelectMany(str => str.ToCharArray())
                .Distinct() // Removes unwanted duplicates from different transitive paths, i.e.,
                            // with part one input to `ABC` -> `A BCEFGH`, you first get `A BCGEFGHCG`
                            // since `B` resolves to `BCGEFGH` and `C` resolves to `CG`
                .ToList();

        unsortedTransitives.Sort(); //Sorts the line of transitives alphabetically

        return string.Join("", unsortedTransitives);
    }

    private static string FindTransitivePathsForDependency(HashSet<char> visited, char transitiveDependency, List<string> dependencies)
    {
        if (!visited.Add(transitiveDependency))
            throw new OverflowException($"Circular dependency on `{transitiveDependency}` detected");

        var line = dependencies.Find(line => line[0] == transitiveDependency);

        // depth first recursion to resolve all sub-dependencies
        return line is null ?
            transitiveDependency.ToString() :
            line[0] + string.Join("", line[1..].Select(transitive => FindTransitivePathsForDependency([..visited], transitive, dependencies)));
    }
}
