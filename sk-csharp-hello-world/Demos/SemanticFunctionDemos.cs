using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;

namespace Demos
{
    public static class SemanticFunctionDemos
    {
        public static async Task CallSemanticFunction(IKernel kernel)
        {
            // Find the folder with all the skills/plugins
            var skillsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

            // Import the plugin
            // NOTE: A plugin can have many functions
            var plugIn = kernel.ImportSemanticSkillFromDirectory(skillsDirectory,       // Path
                                                        "SemanticPlugin");              // Plugin name

            // Add variables to a context
            // These variables will be passed into the function
            var context = new ContextVariables();
            context.Set("input", "stock market");
            context.Set("style", "yoda");

            // Call the semantic function
            var jokeFunctionName = "Joke";
            var result = await kernel.RunAsync(context, plugIn[jokeFunctionName]);
            Console.WriteLine(result);

            // Chain example
            //var excusesFunctionName = "Excuses";
            //var chuckNorrisFunctionName = "ChuckNorris";
            //var chainContext = new ContextVariables();
            //chainContext.Set("input", "karate");
            //ISKFunction[] pipeline = {
            //      //plugIn[excusesFunctionName],
            //      plugIn[chuckNorrisFunctionName]
            //};
            //var chainResult = await kernel.RunAsync(chainContext, pipeline);
            //Console.WriteLine(chainResult);
        }

        public static async Task BasicChaining(IKernel kernel)
        {
            string myJokePrompt = """
            Tell a short joke about {{$INPUT}}.
            """;

            string myPoemPrompt = """
            Take this "{{$INPUT}}" and convert it to a nursery rhyme.
            """;

            string myMenuPrompt = """
            Make this poem "{{$INPUT}}" influence the three items in a coffee shop menu. 
            The menu reads in enumerated form:
            1.
            """;

            // Create inline semantic functions
            var myJokeFunction = kernel.CreateSemanticFunction(myJokePrompt, maxTokens: 500);
            var myPoemFunction = kernel.CreateSemanticFunction(myPoemPrompt, maxTokens: 500);
            var myMenuFunction = kernel.CreateSemanticFunction(myMenuPrompt, maxTokens: 500);

            // Chain the functions together and pass in a context
            var myOutput = await kernel.RunAsync(
                new ContextVariables("Charlie Brown"),
                myJokeFunction,
                myPoemFunction,
                myMenuFunction);

            Console.WriteLine(myOutput);
        }

    }
}