using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.NativePlugin
{    
    public class MyNativePlugin
    {
        [SKFunction("Return the first row of the querty keyboard")]
        [SKFunctionName("QwertyFunctionName")]
        public string Qwerty(string input)
        {
            return "qwertyuiop";
        }

        //[SKFunctionContextParameter(Description = "String to duplicate", Name = "text")]
        [SKFunction("Return a string that's duplicated")]
        [SKFunctionName("DupDup")]
        public string DupDup(string text)
        {
            return text + text;
        }

        [SKFunction("Return a string that's duplicated")]
        [SKFunctionName("DupDupTest")]
        [SKFunctionContextParameter(Name = "text", Description = "String to duplicate")]
        public string DupDupTest(SKContext context)
        {
            var text = context["text"];
            return text + text;
        }

        [SKFunction("Tell a joke")]
        [SKFunctionName("TellAJoke")]
        public async Task<string> TellAJokeAsync(SKContext context) 
        {
            // Fetch a semantic function
            //ISKFunction jokeFunction = context.Func("SemanticPlugin", "Joke");
            //var joke = await jokeFunction.InvokeAsync();
            //return joke.Result.ReplaceLineEndings(" ");

            if (context.Skills != null)
            {
                ISKFunction jokeFunction = context.Skills.GetFunction("SemanticPlugin", "Joke");
                if (jokeFunction != null)
                {
                    var joke = await jokeFunction.InvokeAsync(context);
                    return joke.Result.ReplaceLineEndings(" ");
                }
            }

            return "";
        }
    }
}
