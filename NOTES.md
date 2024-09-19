Part 1

- I started off by writing down test assertions.
  - Considered what if a single letter would be passed in.
  - What if empty array would be passed in,
  - and started building up from simple examples up to the Part one desired input.
- First obstacle I hit was with making sure that even if input lines are not ordered alphabetically,  
where do I put in logic to sort it (so that both `{"DAF", "FH"}` and `{"FH", "DAF"}` -> `{"D AFH", "F H"}`).  
Got there by adding sort in `ResolveTransitive`.
- Lastly, it was the challenge of introducing recursion (I could do it with iteration, 
but since I wanted to achieve the result using only `C# Linq` [library](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/introduction-to-linq-queries), I opted for recursion).  
Hence, the `FindTransitivePathsForDependency` function got created, to encapsulate said recursion :)

Part 2
- Interestingly, I first started off with the part 2 assertion from the brief `{"AB", "BC", "CA"}` to throw `OverflowException`.  
After introducing a check on initial ` char circuitDependency` it worked.
- I then checked if it would work for even simpler case of `{"AB", "BA"}`, it also worked! :D 
- But I wasn't content with it. I knew that it would not throw with non-top level example of `{"AB", "BC", "CB"}`. I was right.  
Even though by now I solved what been asked of me, it was not good in my eyes, I needed to make sure it would correctly throw on any circular dependency found.
- So I ended up modifying the circout breaker logic by tracking a `HashSet<char> visited` for visited dependencies.  
I am also happy with it as, even though it tracks said hashset, it does not violate the idea of Functional Programming, i.e,  
it does not pass around the same instance of the `visited` but each recursion it passes down a newed-up instances, with updates values.