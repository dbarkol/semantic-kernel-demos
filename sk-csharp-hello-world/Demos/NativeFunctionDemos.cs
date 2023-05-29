using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Plugins.NativePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos
{
    public static class NativeFunctionDemos
    {
        public static async Task CallNativeFunction(IKernel kernel)
        { 
            var plugIn = kernel.ImportSkill(new MyNativePlugin(), "MyNativePlugin");

            var context = new ContextVariables();
            context.Set("INPUT", "**This is the input** ");
            var output = await kernel.RunAsync(context, plugIn["DupDup"]);
            Console.WriteLine(output);

            // Pass in context variables
            //var context = new ContextVariables();
            //context.Set("text", "Test with context variables");
            //var output = await kernel.RunAsync(context, plugIn["DupDupTest"]);
            //Console.WriteLine(output);

            // Call a native function from a semantic function
            //var context = new ContextVariables("DoubleTrouble Test");
            //var skillsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            //var semanticPlugin = kernel.ImportSemanticSkillFromDirectory(skillsDirectory, "SemanticPlugin");
            //var output = await kernel.RunAsync(context, semanticPlugin["DoubleTrouble"]);
            //Console.WriteLine(output);

            // Call a semantic function from a native function
            //var context = new ContextVariables();
            //context.Set("style", "yoda");
            //context.Set("input", "dancing");
            //var skillsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            //var semanticPlugin = kernel.ImportSemanticSkillFromDirectory(skillsDirectory, "SemanticPlugin");
            //var output = await kernel.RunAsync(context, plugIn["TellAJoke"]);
            //Console.WriteLine(output);
        }
    }
}
